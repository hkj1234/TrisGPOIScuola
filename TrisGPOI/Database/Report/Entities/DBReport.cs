using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.Report.Entities
{
    public class DBReport
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public string Email { get; set; } = null;
        public string ReportType { get; set; }
        public string ReportTitle { get; set; }
        public string ReportMessage { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
