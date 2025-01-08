using TrisGPOI.Core.Report.Interfaces;
using TrisGPOI.Database.Report.Entities;

namespace TrisGPOI.Core.Report
{
    public class ReportManager : IReportManager
    {
        private readonly IReportRepository _reportRepository;
        public ReportManager(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task<List<DBReport>> GetReports()
        {
            return await _reportRepository.GetReports();
        }
        public async Task<DBReport> GetReport(int id)
        {
            return await _reportRepository.GetReport(id);
        }
        public async Task CreateReport(string email, string type, string title, string message)
        {
            await _reportRepository.CreateReport(email, type, title, message);
        }
        public async Task CreateReportAnonymous(string type, string title, string message)
        {
            await _reportRepository.CreateReportAnonymous(type, title, message);
        }
        public async Task DeleteReport(int id)
        {
            await _reportRepository.DeleteReport(id);
        }
    }
}
