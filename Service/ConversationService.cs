using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Enums;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using AutoMapper;

namespace apicampusjob.Service
{
    public interface IConversationService
    {
        BaseResponseMessage<ConversationDTO> CreateConversation(CreateConversationRequest request);
        BaseResponseMessage<ConversationDTO> GetConversationByUuid(string uuid);
        BaseResponseMessageItem<ConversationDTO> GetConversationsByStudent(string studentUuid);
        BaseResponseMessageItem<ConversationDTO> GetConversationsByCompany(string companyUuid);

    }

    public class ConversationService : BaseService, IConversationService
    {
        private readonly IConversationRepository _conversationRepository;

        public ConversationService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IConversationRepository conversationRepository)
            : base(dbContext, mapper, configuration)
        {
            _conversationRepository = conversationRepository;
        }

        public BaseResponseMessage<ConversationDTO> CreateConversation(CreateConversationRequest request)
        {
            var existing = _conversationRepository.GetByStudentAndCompany(request.StudentUuid, request.CompanyUuid);

            if (existing != null)
            {
                return new BaseResponseMessage<ConversationDTO>
                {
                    Data = _mapper.Map<ConversationDTO>(existing)
                };
            }

            var newConv = new Conversations
            {
                Uuid = Guid.NewGuid().ToString(),
                StudentUuid = request.StudentUuid,
                CompanyUuid = request.CompanyUuid,
                CreatedAt = DateTime.UtcNow
            };

            return ExecuteInTransaction(() =>
            {
                _conversationRepository.CreateItem(newConv);
                return new BaseResponseMessage<ConversationDTO>
                {
                    Data = _mapper.Map<ConversationDTO>(newConv)
                };
            });
        }

        public BaseResponseMessage<ConversationDTO> GetConversationByUuid(string uuid)
        {
            var conversation = _conversationRepository.GetByUuid(uuid);
            if (conversation == null)
            {
                throw new ErrorException(ErrorCode.CONVERSATION_NOT_FOUND);
            }

            return new BaseResponseMessage<ConversationDTO>
            {
                Data = _mapper.Map<ConversationDTO>(conversation)
            };
        }
        public BaseResponseMessageItem<ConversationDTO> GetConversationsByStudent(string studentUuid)
        {
            var response = new BaseResponseMessageItem<ConversationDTO>();
            var data = _conversationRepository.GetConversationsByStudent(studentUuid);
            response.Data = _mapper.Map<List<ConversationDTO>>(data);
            return response;
        }

        public BaseResponseMessageItem<ConversationDTO> GetConversationsByCompany(string companyUuid)
        {
            var response = new BaseResponseMessageItem<ConversationDTO>();
            var data = _conversationRepository.GetConversationsByCompany(companyUuid);
            response.Data = _mapper.Map<List<ConversationDTO>>(data);
            return response;
        }

    }

}
