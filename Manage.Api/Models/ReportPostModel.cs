namespace Manage.Api.Models
{
    public class ReportPostModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
