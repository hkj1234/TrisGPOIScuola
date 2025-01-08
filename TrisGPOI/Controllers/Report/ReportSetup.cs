using TrisGPOI.Core.Report;
using TrisGPOI.Core.Report.Interfaces;
using TrisGPOI.Database.Report;

namespace TrisGPOI.Controllers.Report
{
    public static class ReportSetup
    {
        public static IServiceCollection AddReport(this IServiceCollection services)
        {
            services.AddScoped<IReportManager, ReportManager>();
            services.AddScoped<IReportRepository, ReportRepository>();
            return services;
        }
    }
}
