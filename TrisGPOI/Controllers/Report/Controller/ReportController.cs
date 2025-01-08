using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrisGPOI.Controllers.Report.Entities;
using TrisGPOI.Core.Report.Interfaces;

namespace TrisGPOI.Controllers.Report.Controller
{
    [ApiController]
    [Route("Report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportManager _reportManager;
        public ReportController(IReportManager reportManager)
        {
            _reportManager = reportManager;
        }

        //CreateReport
        [Authorize]
        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] ReportRequest request)
        {
            try
            {
                await _reportManager.CreateReport(User?.Identity?.Name, request.Type, request.Title, request.Message);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //CreateReportAnonymous
        [Authorize]
        [HttpPost("CreateReportAnonymous")]
        public async Task<IActionResult> CreateReportAnonymous([FromBody] ReportRequest request)
        {
            try
            {
                await _reportManager.CreateReportAnonymous(request.Type, request.Title, request.Message);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }
    }
}
