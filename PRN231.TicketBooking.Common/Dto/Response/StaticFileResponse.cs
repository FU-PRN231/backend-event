using System.ComponentModel.DataAnnotations;

namespace PRN231.TicketBooking.Common.Dto.Response
{
    public class StaticFileResponse
    {
        [Key]
        public Guid Id { get; set; }

        public string Url { get; set; }

        public Type StaticFileType { get; set; }

        public enum Type
        {
            Image,
            Video,
            Pdf
        }

        public Guid? SampleProjectId { get; set; }
    }
}