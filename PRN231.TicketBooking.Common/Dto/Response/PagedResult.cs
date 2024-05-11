namespace PRN231.TicketBooking.Common.Dto.Response;

public class PagedResult<T> where T : class
{
    public List<T>? Items { get; set; }
    public int TotalPages { get; set; }
}