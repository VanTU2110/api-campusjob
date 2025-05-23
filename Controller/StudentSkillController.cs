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
    public class StudentSkillController(IStudentSkillService studentSkillService) : BaseController
    {
        private readonly IStudentSkillService _studentSkillService= studentSkillService;
        private readonly DBContext _context;
        /// <summary>
        /// Thêm mới kỹ năng
        /// </summary>
        [HttpPost("create-student-skill")]
        [DbpCert]
        public IActionResult AddStudentSkill([FromBody] UpsertStudentSkillRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _studentSkillService.InsertStudentSkill(request);
                return Ok(response);
            }, _context);
        }
        [HttpPost("get-list-student-skill")]
        [DbpCert]
        public IActionResult GetListStudentSkill([FromBody] GetListStudentSkillByStudentUuid request)
        {
            return ProcessRequest((token) =>
            {
                var response = _studentSkillService.GetListStudentSkill(request);
                return Ok(response);
            },_context);
        }
        [HttpPost("delete-studentskill")]
        [DbpCert]
        public IActionResult DeleteStudentSkill([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _studentSkillService.DeleteStudentSkill(request.Uuid);
                return Ok(response);
            },_context);
        }
    }
}
