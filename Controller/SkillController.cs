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
    public class SkillController(ISkillService skillService, IMapper mapper) : BaseController
    {
        private readonly ISkillService _skillService = skillService;
        private readonly DBContext _context;

        /// <summary>
        /// Lấy danh sách kỹ năng có phân trang
        /// </summary>
        [HttpPost("get-list-page-skill")]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessagePage<SkillDTO>), description: "GetPageListSkill Response")]
        public async Task<IActionResult> GetPageListSkill([FromBody] GetPageListSkillRequest request)
        {
            var response = _skillService.GetPageListSkill(request);
            return Ok(response);
        }

        /// <summary>
        /// Thêm mới kỹ năng
        /// </summary>
        [HttpPost("create-skill")]
        [DbpCert]
        public IActionResult AddSkill([FromBody] UpsertSkillRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _skillService.InsertSkill(request);
                return Ok(response);
            }, _context);
        }

        /// <summary>
        /// Lấy chi tiết kỹ năng
        /// </summary>
        [HttpPost("detail-skill")]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<SkillDTO>), description: "DetailSkill Response")]
        public IActionResult DetailSkill([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _skillService.GetSkillByUuid(request.Uuid);
                return Ok(response);
            }, _context);
        }

        /// <summary>
        /// Cập nhật kỹ năng
        /// </summary>
        [HttpPost("update-skill")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "UpdateSkill Response")]
        public IActionResult UpdateSkill([FromBody] UpsertSkillRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _skillService.UpdateSkill(request, token);
                return Ok(response);
            }, _context);
        }
    }
}
