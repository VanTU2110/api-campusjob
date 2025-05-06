using apicampusjob.AttributeExtend;
using apicampusjob.Controllers;
using apicampusjob.Databases.TM;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace apicampusjob.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [DbpCert]
    public class ApplicationController(IApplicationService applicationService, IMapper mapper, DBContext context) : BaseController
    {
        private readonly IApplicationService _applicationService = applicationService;
        private readonly DBContext _context ;

        /// <summary>
        /// Sinh viên ứng tuyển vào công việc
        /// </summary>
        
        [HttpPost("apply-job")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<ApplicationDTO>), description: "ApplyJob Response")]
        public IActionResult ApplyJob([FromBody] ApplyJobRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _applicationService.ApplyJob(request);
                return Ok(response);
            }, _context);
            
        }

        /// <summary>
        /// Hủy đơn ứng tuyển
        /// </summary>
        [HttpPost("cancel-application")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<ApplicationDTO>), description: "Cancel Application Response")]
        public IActionResult CancelApplication([FromBody] CancellationofApplicationRquest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _applicationService.CancellationofApplication(request);
                return Ok(response);
            }, _context);
           
        }

        /// <summary>
        /// Cập nhật trạng thái ứng tuyển
        /// </summary>
        [HttpPost("update-status")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<ApplicationDTO>), description: "Update Status Response")]
        public IActionResult UpdateStatus([FromBody] UpdateStatusRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _applicationService.UpdateStatus(request);
                return Ok(response);
            },_context);
           
        }

        /// <summary>
        /// Lấy danh sách ứng tuyển theo công việc
        /// </summary>
        [HttpPost("get-list-by-job")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessagePage<ApplicationDTO>), description: "Get Applications By Job Response")]
        public IActionResult GetApplicationsByJob([FromBody] GetPageListApplyByJobUuid request)
        {
            return ProcessRequest((token) =>
            {
                var response = _applicationService.GetPageListApplyByJobUuid(request);
                return Ok(response);
            }, _context);
            
        }

        /// <summary>
        /// Lấy danh sách ứng tuyển theo sinh viên
        /// </summary>
        [HttpPost("get-list-by-student")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessagePage<ApplicationDTO>), description: "Get Applications By Student Response")]
        public IActionResult GetApplicationsByStudent([FromBody] GetPageListApplyByStudentUuid request)
        {
            return ProcessRequest((token) =>
            {
                var response = _applicationService.GetPageListApplyByStudentUuid(request);
                return Ok(response);
            }, _context);
           
        }
        [HttpPost("add-note-to-application")]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "AddNoteToApplication Response")]
        public IActionResult AddNoteToApplication([FromBody] AddNoteToApplicationRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _applicationService.UpdateNote(request);
                return Ok(response);
            },_context) ;
            
        }
    }
}
