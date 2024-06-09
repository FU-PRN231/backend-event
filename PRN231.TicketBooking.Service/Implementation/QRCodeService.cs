using IronBarCode;
using Microsoft.AspNetCore.Http;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class QRCodeService : GenericBackendService ,IQRCodeService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFirebaseService _firebaseService;
        private readonly IUnitOfWork _unitOfWork;   

        public QRCodeService(IServiceProvider serviceProvider,
            IAccountRepository accountRepository,
            IFirebaseService firebaseService,
            IUnitOfWork unitOfWork
            ) : base(serviceProvider)
        {
            _accountRepository = accountRepository; 
            _firebaseService = firebaseService;   
            _unitOfWork = unitOfWork;       
        }

        public async Task<AppActionResult> DecodeQR(string hashedAccountData)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                //string decryptData = DecryptData(hashedAccountData, SD.QR_CODE_KEY);
                //if(decryptData != null)
                //{
                //}
                string[] data = hashedAccountData.Split(',');
                result.Result = new QRAccountResponse
                {
                    FullName = data[0],
                    PhoneNumber = data[1],
                    Email = data[2]
                };
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GenerateQR(string Id)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var accountDb = await _accountRepository.GetById(Id);
                if (accountDb == null)
                {
                    result = BuildAppActionResultError(result, "Không tìm thấy thông tin tài khoản");
                    return result;
                }
                string qrAccountString = $"{accountDb.FirstName} {accountDb.LastName},{accountDb.PhoneNumber},{accountDb.Email}";
                //string encryptAccountResponseString = EncryptData(qrAccountString, SD.QR_CODE_KEY);
                string pathName = SD.FirebasePathName.QR_PREFIX + accountDb.Id;
                IFormFile qr = GenerateQRCodeImage(qrAccountString);
                var url = await _firebaseService.UploadFileToFirebase(qr, pathName);
                if (url.IsSuccess)
                {
                    accountDb.qr = url.Result!.ToString();
                    result.Messages.Add(accountDb.qr);
                    //await _accountRepository.Update(accountDb);
                    await _unitOfWork.SaveChangeAsync();
                }

            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public IFormFile GenerateQRCodeImage(string data)
        {
            GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(data, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium);

            // Save barcode as PNG in memory
            byte[] barcodeBytes = barcode.ToPngBinaryData();

            // Create a MemoryStream from the barcode bytes
            MemoryStream ms = new MemoryStream(barcodeBytes);

            // Create an IFormFile from the MemoryStream
            IFormFile formFile = new FormFile(ms, 0, ms.Length, "barcode.png", "image/png");

            // Set the position of the MemoryStream back to the beginning for subsequent reads
            ms.Position = 0;

            return formFile;
        }

     

        private string EncryptData(string data, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.IV = new byte[16]; // Assuming a zero IV for simplicity
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new System.IO.StreamWriter(cs))
                        {
                            sw.Write(data);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        private string DecryptData(string encryptedData, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = new byte[16];
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;// Assuming a zero IV for simplicity
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(Convert.FromBase64String(encryptedData)))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
