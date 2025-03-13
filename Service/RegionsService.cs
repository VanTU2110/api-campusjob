using AutoMapper;
using apicampusjob.Databases.TM;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;

namespace apicampusjob.Service
{
    public interface IRegionsService
    {
        BaseResponseMessageItem<CategoryAddressDTO> GetPageListProvinsie(GetProvinsie request);
        BaseResponseMessageItem<CategoryAddressDTO> GetPageListDistrictbyProvinsie(GetPageListDistrictbyProvinsieRequest request);
        BaseResponseMessageItem<CategoryAddressDTO> GetPageListCommunebyDistrict(GetPageListCommunebyDistrictRequest request);

    }
    public class RegionsService : BaseService, IRegionsService
    {
        public IRegionsRepository _regionsRepository;
        public RegionsService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IRegionsRepository regionsRepository) : base(dbContext, mapper, configuration)
        {
            _regionsRepository = regionsRepository;
        }

        public BaseResponseMessageItem<CategoryAddressDTO> GetPageListCommunebyDistrict(GetPageListCommunebyDistrictRequest request)
        {
            var respone = new BaseResponseMessageItem<CategoryAddressDTO>();
            var lstProvinsie = _regionsRepository.GetPageListCommunebyDistrict(request);

            if (lstProvinsie != null)
            {
                var lstProvinsieDTO = _mapper.Map<List<CategoryAddressDTO>>(lstProvinsie);

                respone.Data = lstProvinsieDTO;
            }
            return respone;
        }

        public BaseResponseMessageItem<CategoryAddressDTO> GetPageListDistrictbyProvinsie(GetPageListDistrictbyProvinsieRequest request)
        {
            var respone = new BaseResponseMessageItem<CategoryAddressDTO>();
            var lstProvinsie = _regionsRepository.GetPageListDistrictbyProvinsie(request);

            if (lstProvinsie != null)
            {
                var lstProvinsieDTO = _mapper.Map<List<CategoryAddressDTO>>(lstProvinsie);

                respone.Data = lstProvinsieDTO;
            }
            return respone;
        }
            public BaseResponseMessageItem<CategoryAddressDTO> GetPageListProvinsie(GetProvinsie request)
        {
            var respone = new BaseResponseMessageItem<CategoryAddressDTO>();
            var lstProvinsie = _regionsRepository.GetPageListProvinsie(request);
      
            if (lstProvinsie != null )
            {
                var lstProvinsieDTO = _mapper.Map<List<CategoryAddressDTO>>(lstProvinsie);

                respone.Data = lstProvinsieDTO;
            }
            return respone;
        }
    }
}
