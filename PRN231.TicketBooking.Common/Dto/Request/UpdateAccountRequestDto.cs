namespace PRN231.TicketBooking.Common.Dto.Request;

public class UpdateAccountRequestDto
{
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
}