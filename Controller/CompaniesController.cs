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
    public class CompaniesController(ICompaniesService companyService, IMapper mapper) : BaseController
    {
        private readonly ICompaniesService _companyService = companyService;
        private readonly DBContext _context;

        /// <summary>
        /// Tạo mới Company
        /// </summary>
        [HttpPost("create-company")]
        [DbpCert]
        public IActionResult AddCompany([FromBody] UpsertCompaniesRequest company)
        {
            return ProcessRequest((token) =>
            {
                var response = _companyService.InsertCompanies(company);
                return Ok(response);
            }, _context);
        }

        /// <summary>
        /// Lấy thông tin chi tiết Company
        /// </summary>
        [HttpPost("detail-company")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<CompaniesDTO>), description: "DetailCompany Response")]
        public async Task<IActionResult> DetailCompany([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _companyService.GetDetailCompanies(request.Uuid);
                return Ok(response);
            }, _context);
        }
        [HttpPost("detail-company-by-uuid")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<CompaniesDTO>), description: "DetailCompany Response")]
        public async Task<IActionResult> DetailCompanybyCompanyUuid([FromBody] GetDetailCompaniesbyCompanyUuid request)
        {
            return ProcessRequest((token) =>
            {
                var response = _companyService.GetDetailCompaniesbyCompanyUuid(request.CompanyUuid);
                return Ok(response);
            }, _context);
        }
        /// <summary>
        /// Cập nhật thông tin Company
        /// </summary>
        [HttpPost("update-company")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "UpdateCompany Response")]
        public async Task<IActionResult> UpdateCompany([FromBody] UpsertCompaniesRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _companyService.UpdateCompanies(request, token);
                return Ok(response);
            }, _context);
        }
    }

}
