using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Enums;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using apicampusjob.Utils;
using AutoMapper;

namespace apicampusjob.Service
{
    public interface ICompaniesService
    {
        BaseResponse InsertCompanies(UpsertCompaniesRequest company);
        BaseResponse UpdateCompanies(UpsertCompaniesRequest company, TokenInfo token);
        BaseResponseMessage<CompaniesDTO> GetDetailCompanies(string Useruuid);
        BaseResponseMessage<CompaniesDTO> GetDetailCompaniesbyCompanyUuid(string companyUuid);

    }
    public class CompaniesService : BaseService, ICompaniesService
    {
        public ICompaniesRepository _companiesRepository;
        public IUserRepository _userRepository;
        public CompaniesService(DBContext dbContext, IMapper mapper, IConfiguration configuration, ICompaniesRepository companiesRepository, IUserRepository userRepository) : base(dbContext, mapper, configuration)
        {
            _companiesRepository = companiesRepository;
            _userRepository = userRepository;
        }

        public BaseResponseMessage<CompaniesDTO> GetDetailCompanies(string Useruuid)
        {
            var response = new BaseResponseMessage<CompaniesDTO>();
            var company = _companiesRepository.GetCompaniesInforByUserUuid(Useruuid);
            if (company == null)
            {
                throw new ErrorException(ErrorCode.COMPANY_NOT_FOUND);
            }
            var detailcompanyDTO = _mapper.Map<CompaniesDTO>(company);
            response.Data = detailcompanyDTO;
            return response;
        }

        public BaseResponseMessage<CompaniesDTO> GetDetailCompaniesbyCompanyUuid(string companyUuid)
        {
            var response = new BaseResponseMessage<CompaniesDTO>();
            var company = _companiesRepository.GetCompaniesInforbyUuid(companyUuid);
            if (company == null)
            {
                throw new ErrorException(ErrorCode.COMPANY_NOT_FOUND);
            }
            var detailcompanyDTO = _mapper.Map<CompaniesDTO>(company);
            response.Data = detailcompanyDTO;
            return response;
        }

        public BaseResponse InsertCompanies(UpsertCompaniesRequest company)
        {
            var existingUser = _userRepository.GetUserByUuid(company.UserUuid);
            if (existingUser == null)
            {
                throw new ErrorException(ErrorCode.USER_NOT_FOUND);
            }
            if (_companiesRepository.GetCompaniesInforByUserUuid(company.UserUuid) != null)
            {
                throw new ErrorException(ErrorCode.COMPANY_ALREADY_EXIT);
            }
            var newCompany = new Companies
            {
                UserUuid = company.UserUuid,
                Name = company.Name,
                Description = company.Description,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
                Matp = company.Matp,
                Maqh = company.Maqh,
                Xaid = company.Xaid,
            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<CompaniesDTO>
                {
                    Data = _mapper.Map<CompaniesDTO>(_companiesRepository.CreateItem(newCompany))
                };
            });
        }

        public BaseResponse UpdateCompanies(UpsertCompaniesRequest company, TokenInfo token)
        {
            var response = new BaseResponse();
            var oldCompany = _companiesRepository.GetCompaniesInforByUserUuid(company.UserUuid);
            if (oldCompany == null)
            {
                throw new ErrorException(ErrorCode.COMPANY_NOT_FOUND);
            }
            oldCompany.Name = company.Name;
            oldCompany.PhoneNumber = company.PhoneNumber;
            oldCompany.Description = company.Description;
            oldCompany.Matp = company.Matp;
            oldCompany.Maqh = company.Maqh;
            oldCompany.Xaid = company.Xaid;
            _companiesRepository.UpdateItem(company);
            return response;
        }
    }
}
