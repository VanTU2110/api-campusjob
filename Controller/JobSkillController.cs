using apicampusjob.AttributeExtend;
using apicampusjob.Controllers;
using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;
using apicampusjob.Service;
using Microsoft.AspNetCore.Mvc;

namespace apicampusjob.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [DbpCert]
    public class JobSkillController(IJobSkillService jobSkillService) : BaseController
    {
        private readonly IJobSkillService _jobSkillService = jobSkillService;
        private readonly DBContext _context;

        /// <summary>
        /// Thêm kỹ năng yêu cầu cho công việc
        /// </summary>
        [HttpPost("create-job-skill")]
        [DbpCert]
        public IActionResult AddJobSkill([FromBody] UpsertJobSkill request)
        {
            return ProcessRequest((token) =>
            {
                var response = _jobSkillService.InsertJobSkill(request);
                return Ok(response);
            }, _context);
        }
        [HttpPost("delete-job-skill")]
        [DbpCert]
        public IActionResult DeleteJobSKill([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _jobSkillService.DeleteJobSKill(request.Uuid);
                return Ok(response);
            },_context);
        }
    }
}
