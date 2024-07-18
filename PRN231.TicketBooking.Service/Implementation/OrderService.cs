using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Humanizer;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Repository.Implementation;
using PRN231.TicketBooking.Service.Contract;
using PRN231.TicketBooking.Service.Payment.PaymentRequest;
using PRN231.TicketBooking.Service.Payment.PaymentService;
using QRCoder;
using StackExchange.Redis;
using System.Linq;
using System.Net.NetworkInformation;
using System.Transactions;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using Order = PRN231.TicketBooking.BusinessObject.Models.Order;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class OrderService : GenericBackendService, IOrderService
    {
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private IFirebaseService _firebaseService;
        private IUnitOfWork _unitOfWork;

        public OrderService(IServiceProvider serviceProvider, IFirebaseService firebaseService, IMapper mapper, IOrderRepository orderRepository, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            _firebaseService = firebaseService;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppActionResult> CancelOrder(Guid orderId)
        {
            var result = new AppActionResult();
            try
            {
                var orderDb = await _orderRepository.GetById(orderId);
                if (orderDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy đơn hàng với id {orderId}");
                }
                orderDb!.Status = OrderStatus.FAILED;
                await _unitOfWork.SaveChangeAsync();
                result.Messages.Add("Bạn đã hủy đơn đặt hàng thành công");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> CreateOrderWithPayment(OrderRequestDto orderRequestDto, HttpContext context)
        {
            var firebaseService = Resolve<IFirebaseService>();
            var result = new AppActionResult();
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var paymentGatewayService = Resolve<IPaymentGatewayService>();
                    var accountRepository = Resolve<IAccountRepository>();
                    var seatRepository = Resolve<ISeatRankRepository>();
                    var orderDetailsRepository = Resolve<IOrderDetailsRepository>();
                    var accountDb = await accountRepository.GetById(orderRequestDto.AccountId);
                    if (accountDb == null)
                    {
                        result = BuildAppActionResultError(result, $"Tài khoản với {orderRequestDto.AccountId} không tồn tại");
                        return result;
                    }
                    if (string.IsNullOrEmpty(accountDb!.PhoneNumber))
                    {
                        result = BuildAppActionResultError(result, "Số điện thoại của tài khoản không được để trống");
                        return result;
                    }

                    var order = new Order
                    {
                        Id = Guid.NewGuid(),
                        AccountId = accountDb.Id,
                        PurchaseDate = DateTime.Now,
                        Status = OrderStatus.PENDING,
                        Total = 0,
                        Content = orderRequestDto.Content,
                    };

                    double total = 0;

                    foreach (var item in orderRequestDto.SeatRank)
                    {
                        var seatRankDb = await seatRepository!.GetById(item.Id);
                        if (seatRankDb == null)
                        {
                            result = BuildAppActionResultError(result, $"Loại ghế với Id {item.Id} không tồn tại hoặc sản phẩm không đủ số lượng");
                            return result;
                        }
                        var orderDetails = new OrderDetail
                        {
                            Id = Guid.NewGuid(),
                            Quantity = item.Quantity,
                            SeatRankId = item.Id,
                            OrderId = order.Id,
                        };

                        var seatRankItem = seatRankDb;
                        if (seatRankItem.RemainingCapacity < item.Quantity)
                        {
                            result = BuildAppActionResultError(result, $"Số lượng ghế còn lại không đủ cho loại ghế với Id {item.Id}");
                            return result;
                        }

                        seatRankItem.RemainingCapacity -= item.Quantity;

                        await seatRepository.Update(seatRankItem);

                        total += item.Quantity * seatRankItem.Price;
                        await orderDetailsRepository.Insert(orderDetails);
                    }
                    order.Total = total;


                    if (!BuildAppActionResultIsError(result))
                    {
                        await _orderRepository.Insert(order);
                        await _unitOfWork.SaveChangeAsync();
                        scope.Complete();
                    }

                    var payment = new PaymentInformationRequest
                    {
                        AccountID = order.AccountId,
                        Amount = (double)order.Total,
                        CustomerName = $"{order.Account!.FirstName} {order.Account.LastName}",
                        OrderID = order.Id.ToString(),
                    };
                    result.Result = await paymentGatewayService!.CreatePaymentUrlVnpay(payment, context);
                }
                catch (Exception ex)
                {
                    result = BuildAppActionResultError(result, ex.Message);
                }
                return result;
            }
        }

        public async Task<AppActionResult> GenerateTicketQR(Guid orderId)
        {
            var result = new AppActionResult();
            try
            {
                var attendeeRepository = Resolve<IAttendeeRepostory>();
                var orderDetailsRepository = Resolve<IOrderDetailsRepository>();
                var oRCodeService = Resolve<IQRCodeService>();
                var attendeeDb = await attendeeRepository.GetAllAttendee(o => o.OrderDetail.OrderId == orderId, 0, 0, null, false, o => o.OrderDetail.SeatRank);
                Dictionary<string, List<IFormFile>> data = new Dictionary<string, List<IFormFile>>();
                if (attendeeDb.Items.Count > 0)
                {
                    var attendeeImgs = attendeeDb.Items.Where(a => !string.IsNullOrEmpty(a.QR)).Select(o => o.QR).ToList();
                    if (attendeeDb.Items.Count == attendeeImgs.Count)
                    {
                        var groupedImgs = attendeeDb.Items.GroupBy(g => g.OrderDetail!.SeatRank!.Name).ToDictionary(g => g.Key, g => g.Select(i => i.QR).ToList());
                        foreach (var kvp in groupedImgs)
                        {
                            data.Add(kvp.Key,null);
                        }
                    }                    
                } else
                {
                    var insertAttendee = await AddAttendee(orderId);
                    if (insertAttendee == null)
                    {
                        result = BuildAppActionResultError(result, $"Tạo bảng điểm danh cho order với id {orderId} thất bại, vui lòng thử lại");
                    }

                    Dictionary<Guid, string> details = new Dictionary<Guid, string>();
                    string seatRankName = null;
                    foreach (var attendee in insertAttendee!)
                    {
                        AppActionResult upload = new AppActionResult();
                        upload = await oRCodeService.GenerateQR(attendee.Id.ToString());
                        if (upload == null)
                        {
                            result = BuildAppActionResultError(result, $"Không thể tải hình ảnh vé, vui lòng thử lại");
                            return result;
                        }
                        UploadImgResponseDto QrCodeDto = (UploadImgResponseDto)upload.Result;
                        attendee.QR = QrCodeDto.url;
                        await attendeeRepository.Insert(attendee);
                        if(!details.ContainsKey(attendee.OrderDetailId)) 
                        {
                            var detail = await orderDetailsRepository.GetById(attendee.OrderDetailId);
                            if(detail == null)
                            {
                                result = BuildAppActionResultError(result, $"Không tìm thấy ghế ngồi của chi tiết đơn hàng {attendee.OrderDetailId}");
                                return result;
                            }
                            details.Add(attendee.OrderDetailId, detail.SeatRank.Name);
                        }
                        seatRankName = details[attendee.OrderDetailId];
                        if (data.ContainsKey(seatRankName))
                        {
                            data[seatRankName].Add(QrCodeDto.file);
                        } else
                        {                         
                            data.Add(seatRankName, new List<IFormFile> { QrCodeDto.file });
                        }
                    }
                }
                await _unitOfWork.SaveChangeAsync();
                result.Result = data;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
      
        /* private async Task<UploadImgResponseDto> GenerateQR(string Id)
         {
             UploadImgResponseDto result = null;
             try
             {
                 string pathName = SD.FirebasePathName.QR_PREFIX + Id;
                 //IFormFile qr = GenerateQRCodeImage(Id);
                 IFormFile qr = CreateQRCode(Id);
                 var url = await _firebaseService.UploadFileToFirebase(qr, pathName);
                 if (url.IsSuccess)
                 {
                     result.file = qr;
                     result.url = (string)url.Result;
                 }
                 else
                 {
                     return null;
                 }

             }
             catch (Exception ex)
             {
                 result = null;
             }
             return result;
         }*/

        /*  private IFormFile GenerateQRCodeImage(string data)
          {
              var barcodeWriter = new BarcodeWriterPixelData
              {
                  Format = BarcodeFormat.QR_CODE,
                  Options = new EncodingOptions
                  {
                      Height = 500,
                      Width = 500,
                      Margin = 1
                  }
              };

              var pixelData = barcodeWriter.Write(data);

              using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
              {
                  var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bitmap.PixelFormat);
                  try
                  {
                      System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                  }
                  finally
                  {
                      bitmap.UnlockBits(bitmapData);
                  }

                  var memoryStream = new MemoryStream();
                  bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                  memoryStream.Position = 0;

                  IFormFile formFile = new FormFile(memoryStream, 0, memoryStream.Length, "barcode.png", "image/png");

                  return formFile;
              }
          }*/


        public async Task<AppActionResult> GetAllOrder(int pageNumber, int pageSize)
        {
            var result = new AppActionResult();
            var orderResponseList = new List<OrderResponses>();
            try
            {
                var orderDetailsRepository = Resolve<IOrderDetailsRepository>();
                var ordersDb = await _orderRepository.GetAllDataByExpression(null, pageNumber, pageSize, null, false, p => p.Account!);
                if (ordersDb == null)
                {
                    result = BuildAppActionResultError(result, "Không tìm thấy đơn hàng nào");
                }
                if (ordersDb!.Items!.Count > 0 && ordersDb.Items != null)
                {
                    foreach (var item in ordersDb.Items)
                    {
                        var orderResponse = new OrderResponses();
                        var orderDetailsDb = await orderDetailsRepository.GetAllOrderDetail(p => p.OrderId == item.Id, 0, 0, null, false, p => p.SeatRank!.Event!);
                        orderResponse.Order = item;
                        orderResponse.OrderDetails = orderDetailsDb.Items!;
                        orderResponseList.Add(orderResponse);
                    }
                    result.Result = new PagedResult<OrderResponses>
                    {
                        Items = orderResponseList,
                        TotalPages = ordersDb.TotalPages
                    };
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAllOrderByAccountId(string accountId, int pageNumber, int pageSize)
        {
            var result = new AppActionResult();
            var orderResponseList = new List<OrderResponses>();
            try
            {
                var orderDetailsRepository = Resolve<IOrderDetailsRepository>();
                var ordersDb = await _orderRepository.GetAllOrder(p => p.AccountId == accountId, pageNumber, pageSize, p => p.PurchaseDate, false, p => p.Account!);
                if (ordersDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy đơn hàng nào");
                }
                if (ordersDb!.Items!.Count > 0 && ordersDb.Items != null)
                {
                    foreach (var item in ordersDb.Items)
                    {
                        var orderResponse = new OrderResponses();
                        var orderDetailsDb = await orderDetailsRepository.GetAllOrderDetail(p => p.OrderId == item.Id, 0, 0, null, false, p => p.SeatRank!.Event!);
                        orderResponse.Order = item;
                        orderResponse.OrderDetails = orderDetailsDb.Items!;
                        orderResponseList.Add(orderResponse);
                    }
                    result.Result = new PagedResult<OrderResponses>
                    {
                        Items = orderResponseList,
                        TotalPages = ordersDb.TotalPages
                    };
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAllOrderByEventId(Guid eventId, int pageNumber, int pageSize)
        {
            var result = new AppActionResult();
            var orderResponseList = new List<OrderResponses>();
            try
            {
                var orderDetailsRepository = Resolve<IOrderDetailsRepository>();
                var orderDetailsDb = await orderDetailsRepository.GetAllOrderDetail(p => p.SeatRank!.EventId == eventId, pageNumber, pageSize, null, false, p => p.Order!);
                if (orderDetailsDb == null)
                {
                    result = BuildAppActionResultError(result, $"Chi tiết đơn hàng này không tìm thấy với sự kiện với {eventId}");
                }
                var order = orderDetailsDb!.Items!.Select(p => p.Order);
                foreach (var item in order)
                {
                    var orderDetails = await orderDetailsRepository.GetAllOrderDetail(p => p.SeatRank!.EventId == eventId, pageNumber, pageSize, null, false, p => p.Order!, p => p.SeatRank!.Event!);
                    orderResponseList.Add(new OrderResponses
                    {
                        Order = item!,
                        OrderDetails = orderDetails.Items!
                    });
                }
                result.Result = new PagedResult<OrderResponses>
                {
                    Items = orderResponseList,
                    TotalPages = orderDetailsDb.TotalPages,
                };
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAllOrderByStatus(OrderStatus orderStatus, int pageNumber, int pageSize)
        {
            var result = new AppActionResult();
            var orderResponseList = new List<OrderResponses>();
            try
            {
                var orderDetailsRepository = Resolve<IOrderDetailsRepository>();
                var ordersDb = await _orderRepository.GetAllOrder(p => p.Status == orderStatus, pageNumber, pageSize, null, false, p => p.Account!);
                if (ordersDb == null)
                {
                    result = BuildAppActionResultError(result, "Không tìm thấy đơn hàng nào");
                }
                if (ordersDb!.Items!.Count > 0 && ordersDb.Items != null)
                {
                    foreach (var item in ordersDb.Items)
                    {
                        var orderResponse = new OrderResponses();
                        var orderDetailsDb = await orderDetailsRepository.GetAllDataByExpression(p => p.OrderId == item.Id, 0, 0, null, false, p => p.SeatRank!.Event!);
                        orderResponse.Order = item;
                        orderResponse.OrderDetails = orderDetailsDb.Items!;
                        orderResponseList.Add(orderResponse);
                    }
                    result.Result = new PagedResult<OrderResponses>
                    {
                        Items = orderResponseList,
                        TotalPages = ordersDb.TotalPages
                    };
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAllOrderDetailByOrderId(Guid orderId, int pageNumber, int pageSize)
        {
            var result = new AppActionResult();
            try
            {
                var orderDetailsRepository = Resolve<IOrderDetailsRepository>();
                var orderDb = await _orderRepository!.GetAllOrder(p => p.Id == orderId, 0, 0, null, false, p => p.Account!);
                if (orderDb == null)
                {
                    result = BuildAppActionResultError(result, $"Đơn hàng với {orderDb} không tồn tại");
                }
                if (!BuildAppActionResultIsError(result))
                {
                    var orderDetailsDb = await orderDetailsRepository.GetAllOrderDetail(p => p!.OrderId == orderId, pageNumber, pageSize, null, false, p => p.SeatRank!.Event!);
                    OrderResponses orderResponses = new OrderResponses();
                    orderResponses.OrderDetails = orderDetailsDb!.Items!;
                    orderResponses.Order = orderDb.Items!.FirstOrDefault()!;
                    result.Result = orderResponses;
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetEventOrderByStatus(Guid eventId, OrderStatus orderStatus, int pageNumber, int pageSize)
        {
            var result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var orderDetailsRepository = Resolve<IOrderDetailsRepository>();
                var eventDb = eventRepository.GetById(eventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Sự kiện với {eventId} không tồn tại");
                }
                var orderDetailsDb = await orderDetailsRepository.GetAllOrderDetail(p => p.SeatRank!.EventId == eventId, pageNumber, pageSize, null, false, p => p.Order!);
                if (orderDetailsDb == null)
                {
                    result = BuildAppActionResultError(result, $"Chi tiết đơn hàng này không tìm thấy với sự kiện với {eventId}");
                }
                if (orderDetailsDb!.Items != null && orderDetailsDb.Items.Count > 0)
                {
                    var orderIds = orderDetailsDb.Items.DistinctBy(o => o.OrderId).Select(o => o.OrderId);
                    result.Result = await _orderRepository.GetAllOrder(p => p.Status == orderStatus && orderIds.Contains(p.Id), pageNumber, pageSize, null, false, p => p.Account!);
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> PurchaseOrder(Guid orderId, HttpContext context)
        {
            var result = new AppActionResult();
            try
            {
                var paymentGatewayService = Resolve<IPaymentGatewayService>();
                var orderDb = await _orderRepository.GetById(orderId);
                if (orderDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy đơn hàng với id {orderId}");
                }
                if (orderDb!.Status == OrderStatus.PENDING)
                {
                    var payment = new PaymentInformationRequest
                    {
                        AccountID = orderDb.AccountId,
                        Amount = (double)orderDb.Total,
                        CustomerName = $"{orderDb.Account!.FirstName} {orderDb.Account.LastName}",
                        OrderID = orderDb.Id.ToString(),
                    };
                    result.Result = await paymentGatewayService!.CreatePaymentUrlVnpay(payment, context);
                }
                else
                {
                    result.Messages.Add("Đơn hàng này đã được thanh toán hoặc đã hủy");
                    result.IsSuccess = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;

        }

        public async Task<AppActionResult> SendTicketEmail(Guid orderId)
        {
            var result = new AppActionResult();
            try
            {
                var orderDb = await _orderRepository.GetById(orderId);
                if (orderDb == null)
                {
                    result = BuildAppActionResultError(result, $"Đơn hàng với id {orderId} không tồn tại");
                    return result;
                }
                var orderDetailRepository = Resolve<IOrderDetailsRepository>();
                var orderDetailDb = await orderDetailRepository.GetAllOrderDetail(p => p!.OrderId == orderId && p.Order!.Status == OrderStatus.SUCCUSSFUL, 0, 0, null, false, o => o.Order.Account, o => o.SeatRank.Event.Location, o => o.SeatRank.Event.Organization);
                if (orderDetailDb.Items!.Count == 0)
                {
                    result = BuildAppActionResultError(result, $"Chi tiết của đơn hàng thành công với id {orderId} không tồn tại");
                    return result;
                }

                Dictionary<string, List<IFormFile>> ticketInfo = new Dictionary<string, List<IFormFile>>();
                var ticketData = await GenerateTicketQR(orderId);
                if (!ticketData.IsSuccess)
                {
                    result.Messages.AddRange(ticketData.Messages);
                    result.IsSuccess = false;
                    return result;
                }
                ticketInfo = (Dictionary<string, List<IFormFile>>)ticketData.Result!;

                var emailService = Resolve<IEmailService>();
                Account account = orderDetailDb!.Items!.FirstOrDefault()!.Order!.Account!;
                Event eventDb = orderDetailDb!.Items!.FirstOrDefault()!.SeatRank!.Event!;
                string body = TemplateMappingHelper.GenerateTicketEmailBody(account, ticketInfo, eventDb);
                List<IFormFile> files = new List<IFormFile>();
                foreach (var kvp in ticketInfo)
                {
                    foreach (var img in kvp.Value)
                    {
                        files.Add(img);
                    }
                }
                emailService!.SendEmailWithFiles(account.Email, SD.SubjectMail.SEAT_TICKET, body, files);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> UpdateStatus(Guid orderId, bool isSuccessful)
        {
            var result = new AppActionResult();
            try
            {
                var orderDb = await _orderRepository.GetById(orderId);
                if (orderDb == null)
                {
                    result = BuildAppActionResultError(result, "Đơn hàng này không tồn tại");
                }
                else
                {
                    //if (!isSuccessful) orderDb.Status = OrderStatus.CANCELLED;
                    if (orderDb.Status == OrderStatus.PENDING)
                    {
                        orderDb.Status = OrderStatus.SUCCUSSFUL;
                        await _unitOfWork.SaveChangeAsync();
                        var orderSend = await this.SendTicketEmail(orderDb.Id);
                        if (!orderSend.IsSuccess)
                        {
                            foreach (var mess in orderSend.Messages)
                            {
                                result.Messages.Add(mess);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        private async Task<List<Attendee>> AddAttendee(Guid orderId)
        {
            List<Attendee> data = new List<Attendee>();
            try
            {
                var orderDetailRepository = Resolve<IOrderDetailsRepository>();
                var attendeeRepository = Resolve<IAttendeeRepostory>();
                var orderDetailDb = await orderDetailRepository.GetAllOrderDetail(o => o.OrderId == orderId, 0, 0, null, false, o => o.SeatRank);
                if (orderDetailDb.Items.Count > 0)
                {
                    int i = 0;
                    foreach (var detail in orderDetailDb.Items)
                    {
                        i = detail.Quantity;
                        while (i > 0)
                        {
                            data.Add(new Attendee
                            {
                                Id = Guid.NewGuid(),
                                CheckedIn = false,
                                OrderDetailId = detail.Id,
                                QR = string.Empty
                            });
                            i--;
                        }
                    }
                }
            } catch(Exception ex)
            {
                data = null;
            }
            return data;
        }
    }
}
