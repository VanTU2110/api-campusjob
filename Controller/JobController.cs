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
    public class JobController(IJobService jobService, IMapper mapper) : BaseController
    {
        private readonly IJobService _jobService = jobService;
        private readonly DBContext _context;

        /// <summary>
        /// Lấy danh sách công việc có phân trang
        /// </summary>
        [HttpPost("get-list-page-job")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessagePage<JobDTO>), description: "GetPageListJob Response")]
        public async Task<IActionResult> GetPageListJob([FromBody] GetPageListJobRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _jobService.GetPageListJob(request);
                return Ok(response);
            }, _context);
        }
        [HttpPost("search-by-skill")]
        [DbpCert]
        public async Task<IActionResult> SearchBySkill([FromBody] SearchJobBySkillRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _jobService.GetJobBySkill(request);
                return Ok(response);
            },_context);
            
        }
        [HttpPost("search-by-schedule")]
        [DbpCert]
        public async Task<IActionResult> SearchBySchedule([FromBody] GetJobsByScheduleRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _jobService.GetJobBySchedule(request);
                return Ok(response);
            }, _context);

        }

        /// <summary>
        /// Thêm mới công việc
        /// </summary>
        [HttpPost("create-job")]
        [DbpCert]
        public IActionResult AddJob([FromBody] UpsertJobRequest job)
        {
            return ProcessRequest((token) =>
            {
                var response = _jobService.InsertJob(job);
                return Ok(response);
            }, _context);
        }

        /// <summary>
        /// Lấy chi tiết công việc
        /// </summary>
        [HttpPost("detail-job")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<JobDTO>), description: "DetailJob Response")]
        public async Task<IActionResult> DetailJob([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _jobService.GetJobByUuid(request.Uuid);
                return Ok(response);
            }, _context);
        }

        /// <summary>
        /// Cập nhật công việc
        /// </summary>
        [HttpPost("update-job")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "UpdateJob Response")]
        public async Task<IActionResult> UpdateJob([FromBody] UpsertJobRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _jobService.UpdateJob(request, token);
                return Ok(response);
            }, _context);
        }
    }
}