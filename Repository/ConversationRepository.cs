using apicampusjob.Databases.TM;

namespace apicampusjob.Repository
{
    public interface IConversationRepository : IBaseRepository
    {
        Conversations? GetByStudentAndCompany(string studentUuid, string companyUuid);
        Conversations? GetByUuid(string uuid);
        List<Conversations> GetConversationsByStudent(string studentUuid);
        List<Conversations> GetConversationsByCompany(string companyUuid);

    }

    public class ConversationRepository : BaseRepository, IConversationRepository
    {
        public ConversationRepository(DBContext dbContext) : base(dbContext) { }

        public Conversations? GetByStudentAndCompany(string studentUuid, string companyUuid)
        {
            return _dbContext.Conversations
                .FirstOrDefault(c => c.StudentUuid == studentUuid && c.CompanyUuid == companyUuid);
        }

        public Conversations? GetByUuid(string uuid)
        {
            return _dbContext.Conversations.FirstOrDefault(c => c.Uuid == uuid);
        }
        public List<Conversations> GetConversationsByStudent(string studentUuid)
        {
            return _dbContext.Conversations
                .Where(c => c.StudentUuid == studentUuid)
                .ToList();
        }

        public List<Conversations> GetConversationsByCompany(string companyUuid)
        {
            return _dbContext.Conversations
                .Where(c => c.CompanyUuid == companyUuid)
                .ToList();
        }

    }

}
