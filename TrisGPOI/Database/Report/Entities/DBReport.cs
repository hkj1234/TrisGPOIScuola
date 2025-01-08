using System.ComponentModel.DataAnnotations;

namespace TrisGPOI.Database.Report.Entities
{
    public class DBReport
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string ReportType { get; set; }
        public string ReportTitle { get; set; }
        public string ReportMessage { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
