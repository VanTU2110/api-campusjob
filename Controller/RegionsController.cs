using AutoMapper;
using apicampusjob.AttributeExtend;
using apicampusjob.Controllers;
using apicampusjob.Databases.TM;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using apicampusjob.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace apicampusjob.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController(IRegionsService regionsService, IMapper mapper) : BaseController
    {
        private readonly IRegionsService _regionsService = regionsService;
        private readonly DBContext _context;
        [HttpPost("get-list-page-provinsie")]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessagePage<CategoryAddressDTO>), description: "GetPageListProvinsie Respone")]
        public async Task<IActionResult> GetPageListProvinsie([FromBody] GetProvinsie request)
        {
            var response = _regionsService.GetPageListProvinsie(request);
            return Ok(response);
        }
        [HttpPost("get-list-page-district-by-provinsie")]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessagePage<CategoryAddressDTO>), description: "GetPageListDistrictbyProvinsie Response")]
        public async Task<IActionResult> GetPageListDistrictbyProvinsie([FromBody] GetPageListDistrictbyProvinsieRequest request)
        {
            var response = _regionsService.GetPageListDistrictbyProvinsie(request);
            return Ok(response);
        }

        [HttpPost("get-list-page-commune-by-district")]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessagePage<CategoryAddressDTO>), description: "GetPageListCommunebyDistrict Response")]
        public async Task<IActionResult> GetPageListCommunebyDistrict([FromBody] GetPageListCommunebyDistrictRequest request)
        {
            var response = _regionsService.GetPageListCommunebyDistrict(request);
            return Ok(response);
        }
    }
}
