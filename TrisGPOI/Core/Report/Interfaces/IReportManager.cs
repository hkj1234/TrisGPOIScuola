using TrisGPOI.Database.Report.Entities;

namespace TrisGPOI.Core.Report.Interfaces
{
    public interface IReportManager
    {
        Task<List<DBReport>> GetReports();
        Task<DBReport> GetReport(int id);
        Task CreateReport(string email, string type, string title, string message);
        Task CreateReportAnonymous(string type, string title, string message);
        Task DeleteReport(int id);
    }
}
