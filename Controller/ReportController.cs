using apicampusjob.AttributeExtend;
using apicampusjob.Controllers;
using apicampusjob.Databases.TM;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apicampusjob.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [DbpCert]
    public class ReportController(IReportService reportService, IMapper mapper) : BaseController
    {
        private readonly IReportService _reportService = reportService;
        private readonly DBContext _context;

        /// <summary>
        /// Lấy danh sách kỹ năng có phân trang
        /// </summary>
        [HttpPost("get-list-page-report")]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessagePage<ReportDTO>), description: "GetPageListReport Response")]
        public async Task<IActionResult> GetPageListReport([FromBody] GetPageListReport request)
        {
            return ProcessRequest((token) =>
            {
                var response = _reportService.GetPageListReport(request);
                return Ok(response);
            }, _context);
           
        }

        /// <summary>
        /// Tao moi bao cao
        /// </summary>
        [HttpPost("create-report")]
        [DbpCert]
        public IActionResult CreateReport([FromBody] CreateReportRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _reportService.CreateReport(request);
                return Ok(response);
            }, _context);
        }
        [HttpPost("detail-report")]
        [DbpCert]
        public IActionResult DetailReport([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _reportService.GetDetailReport(request.Uuid);
                return Ok(response);
            }, _context);
        }
        [HttpPost("update-status-report")]
        [DbpCert]
        public IActionResult UpdateStatusReport([FromBody] UpdateReportStatus request)
        {
            return ProcessRequest((token) =>
            {
                var response = _reportService.UpdateStatus(request);
                return Ok(response);
            }, _context);
        }
    }
}
