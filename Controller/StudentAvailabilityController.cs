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
    public class StudentAvailabilityController(IStudentAvailabilityService studentAvailabilityService, IMapper mapper) : BaseController
    {
        private readonly IStudentAvailabilityService _studentAvailabilityService = studentAvailabilityService;
        private readonly DBContext _context;


        /// <summary>
        /// 📌 Thêm mới thời gian rảnh
        /// </summary>
        [HttpPost("insert-availability")]
        [DbpCert]
        public IActionResult InsertAvailability([FromBody] UpsertStudentAvailability request)
        {
            return ProcessRequest((token) =>
            {
                var response = _studentAvailabilityService.InsertStudentAvailability(request);
                return Ok(response);
            }, _context);
        }

        /// <summary>
        /// 📌 Cập nhật thời gian rảnh
        /// </summary>
        [HttpPost("update-availability")]
        [DbpCert]
        public IActionResult UpdateAvailability([FromBody] UpsertStudentAvailability request)
        {
            return ProcessRequest((token) =>
            {
                var response = _studentAvailabilityService.UpdateStudentAvailability(request);
                return Ok(response);
            }, _context);
        }
        [HttpPost("get-list-vailability")]
        [DbpCert]
        public IActionResult GetListAvailability([FromBody] GetAvailabilityByStudenUuid request)
        {
            return ProcessRequest((token) =>
            {
                var response = _studentAvailabilityService.GetListAvailability(request);
                return Ok(response);
            },_context);
        }

        
    }
}
