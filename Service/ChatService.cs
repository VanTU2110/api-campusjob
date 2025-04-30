using apicampusjob.Databases.TM;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using AutoMapper;

namespace apicampusjob.Service
{
    public interface IChatService
    {

        BaseResponse SendMessage(SendMessageRequest request);
        BaseResponseMessageItem<MessageDTO> GetConversationMessages(GetMessagesByConversation request);
    }
    public class ChatService : BaseService, IChatService
    {
        private readonly IMessageRepository _messageRepository;
        public ChatService(DBContext dbContext, IMapper mapper, IConfiguration configuration,IMessageRepository messageRepository) : base(dbContext, mapper, configuration)
        {
            _messageRepository = messageRepository;
        }

        public BaseResponseMessageItem<MessageDTO> GetConversationMessages(GetMessagesByConversation request)
        {
            var response = new BaseResponseMessageItem<MessageDTO>();
            var lstMessage = _messageRepository.GetMessagesByConversation(request);
            if (lstMessage != null)
            {
                var lstMessageDTO = _mapper.Map<List<MessageDTO>>(lstMessage);
                response.Data = lstMessageDTO;
            }
            return response;

        }

        public BaseResponse SendMessage(SendMessageRequest request)
        {
            var newMessage = new Messages
            {
                ConversationUuid = request.ConversationUuid,
                SenderUuid = request.SenderUuid,
                Content = request.Content,
            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<MessageDTO>
                {
                    Data = _mapper.Map<MessageDTO>(_messageRepository.CreateItem(newMessage))
                };
            });
        }
    }
}
