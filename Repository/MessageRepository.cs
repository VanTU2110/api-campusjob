using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;

namespace apicampusjob.Repository
{
    public interface IMessageRepository : IBaseRepository
    {
        List<Messages> GetMessagesByConversation(GetMessagesByConversation request);
    }
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<Messages> GetMessagesByConversation(GetMessagesByConversation request)
        {
            return _dbContext.Messages
            .Where(m => m.ConversationUuid == request.ConversationUuid)
            .OrderBy(m => m.SendAt)
            .ToList();
        }
    }
}
