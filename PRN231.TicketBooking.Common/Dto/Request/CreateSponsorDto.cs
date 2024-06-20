using Microsoft.AspNetCore.Http;

namespace PRN231.TicketBooking.Common.Dto.Request
{
	public class CreateSponsorDto
	{
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public string Email { get; set; } = null!;
		public IFormFile Img { get; set; } = null!;
	}

}