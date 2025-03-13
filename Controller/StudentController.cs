using apicampusjob.AttributeExtend;
using apicampusjob.Controllers;
using apicampusjob.Databases.TM;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace apicampusjob.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [DbpCert]
    public class StudentController(IStudentService studentService, IMapper mapper) : BaseController
    {
        private readonly IStudentService _studentService= studentService;
        private readonly DBContext _context;
     
        [HttpPost("create-student")]
        [DbpCert]
        public IActionResult AddStudent([FromBody] UpsertStudentRequest student)
        {
            return ProcessRequest((token) =>
            {
                var response = _studentService.InsertStudent(student);
                return Ok(response);
            }, _context);
        }

        /// <summary>
        /// Lấy thông tin chi tiết Student
        /// </summary>
        [HttpPost("detail-student")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<StudentDTO>), description: "DetailStudent Response")]
        public async Task<IActionResult> DetailStudent([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _studentService.GetDetailStudent(request.Uuid);
                return Ok(response);
            }, _context);
        }

        /// <summary>
        /// Cập nhật thông tin Student
        /// </summary>
        [HttpPost("update-student")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "UpdateStudent Response")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpsertStudentRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _studentService.UpdateStudent(request, token);
                return Ok(response);
            }, _context);
        }
    }
}
