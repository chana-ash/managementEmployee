namespace Manage.Api.Models
{
    public class VacationPostModel
    {
        public int Id { get; set; }


        public int UserId { get; set; }
        public DateTime ReportDate { get; set; }
        public string Request { get; set; }
    }
}
