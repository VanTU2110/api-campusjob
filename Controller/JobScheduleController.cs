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
    public class JobScheduleController(IJobScheduleService jobScheduleService, IMapper mapper) : BaseController
    {
        private readonly IJobScheduleService _jobScheduleService = jobScheduleService;
        private readonly DBContext _context;

        /// <summary>
        /// 📌 Lấy danh sách lịch làm việc của các công việc
        /// </summary>
        [HttpPost("get-list-job-schedule")]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<List<JobScheduleDTO>>), description: "GetJobSchedule Response")]
        public async Task<IActionResult> GetJobSchedules([FromBody] GetPageListSchedule request)
        {
            var response = _jobScheduleService.GetPageListSchedule(request);
            return Ok(response);
        }
        /// <summary>
        /// 📌 Lấy tất cả lịch làm việc của một công việc cụ thể
        /// </summary>
        [HttpPost("get-detail-job-schedule")]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessageItem<ScheduleInfoCatalogDTO>), description: "GetDetailJobSchedule Response")]
        public async Task<IActionResult> GetDetailJobSchedule([FromBody] GetListJobScheduleByJobUuid request)
        {
            return ProcessRequest((token) =>
            {
                var response = _jobScheduleService.GetDetailSchedule(request);
                return Ok(response);
            }, _context);
        }
        /// <summary>
        /// 📌 Them một ca làm việc cụ thể
        /// </summary>
        [HttpPost("insert-job-schedule")]
        [DbpCert]
        public IActionResult InsertJobSchedule([FromBody] UpsertScheduleRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _jobScheduleService.InsertSchedule(request);
                return Ok(response);
            }, _context);
        }

        /// <summary>
        /// 📌 Cập nhật một ca làm việc cụ thể
        /// </summary>
        [HttpPost("update-job-schedule")]
        [DbpCert]
        public IActionResult UpdateJobSchedule([FromBody] UpsertScheduleRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _jobScheduleService.UpdateSchedule(request,token);
                return Ok(response);
            }, _context);
        }

        
    }
}