using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.Report.Interfaces;
using TrisGPOI.Database.Context;
using TrisGPOI.Database.Report.Entities;

namespace TrisGPOI.Database.Report
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public ReportRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<List<DBReport>> GetReports()
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            return await context.Report.ToListAsync();
        }
        public async Task<DBReport> GetReport(int id)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            return await context.Report.FindAsync(id);
        }
        public async Task CreateReport(string email, string type, string title, string message)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            await context.Report.AddAsync(new DBReport { Email = email, ReportType = type, ReportTitle = title, ReportMessage = message, ReportDate = DateTime.UtcNow });
            await context.SaveChangesAsync();
        }
        public async Task CreateReportAnonymous(string type, string title, string message)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            await context.Report.AddAsync(new DBReport { Email = "", ReportType = type, ReportTitle = title, ReportMessage = message, ReportDate = DateTime.UtcNow });
            await context.SaveChangesAsync();
        }
        public async Task DeleteReport(int id)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            var temp = await context.Report.FindAsync(id);
            context.Report.Remove(temp);
            await context.SaveChangesAsync();
        }
    }
}
