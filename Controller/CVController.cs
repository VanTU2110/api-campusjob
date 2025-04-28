using apicampusjob.AttributeExtend;
using apicampusjob.Controllers;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Databases.TM;
using apicampusjob.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apicampusjob.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [DbpCert]
    public class CVController(ICVService cvService, IMapper mapper) : BaseController
    {
        private readonly ICVService _cvService = cvService;
        private readonly DBContext _context;
        /// <summary>
        /// 📌 Lấy danh sách CV của học sinh
        /// </summary>
        [HttpPost("get-list-cv-student")]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<List<CVDTO>>), description: "GetListCV Response")]
        public async Task<IActionResult> GetCVs([FromBody] GetCVByStudenUuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _cvService.GetCVByStudentUui(request);
                return Ok(response);
            }, _context);
            
        }

        /// <summary>
        /// 📌 Lấy chi tiết CV của một học sinh
        /// </summary>
        [HttpPost("get-detail-cv")]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessageItem<CVDTO>), description: "GetDetailCV Response")]
        public async Task<IActionResult> GetDetailCV([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _cvService.GetCVByUuid(request.Uuid);
                return Ok(response);
            }, _context);
        }

        /// <summary>
        /// 📌 Thêm một CV mới
        /// </summary>
        [HttpPost("insert-cv")]
        [DbpCert]
        public IActionResult InsertCV([FromBody] InsertCVRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _cvService.InsertCV(request);
                return Ok(response);
            }, _context);
        }
    }
}
