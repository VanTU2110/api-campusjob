using apicampusjob.Databases.TM;
using Microsoft.EntityFrameworkCore;

namespace apicampusjob.Repository
{
    public interface IUserRepository : IBaseRepository
    {
        User GetUserByUuid(string uuid);
        User GetUserByEmail(string email);
    }
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DBContext dBContext) : base(dBContext) { }

        public User GetUserByEmail(string email)
        {
            return _dbContext.User.FirstOrDefault(x => x.Email == email);
        }

        public User GetUserByUuid(string uuid)
        {
            return _dbContext.User.FirstOrDefault(x => x.Uuid == uuid);
        }
    }
}
