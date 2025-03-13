using Microsoft.EntityFrameworkCore;

using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;
using apicampusjob.Repository;

namespace secondhand_market.Repository
{
    public interface ISessionRepository :IBaseRepository
    {
        bool LogOutAllSession(string username);
        List<Sessions?> GetListSessionByAccountUuid(string accountUuid);

        Sessions? GetSessionByUuid(string token);
    }
    public class SessionRepository : BaseRepository,ISessionRepository
    {
        public SessionRepository(DBContext dbContext) : base(dbContext)
        {
        }


        public bool LogOutAllSession(string email)
        {
            var session = _dbContext.Sessions.Include(x=>x.UserUu).Where(x => x.UserUu.Email == email && x.Status == 0).ToList();

            if (session != null && session.Count() > 0)
            {
                foreach (var item in session)
                {
                    if (item.Status == 0)
                    {
                        item.TimeLogout = DateTime.Now;
                        item.Status = 1;
                    }

                }
                _dbContext.SaveChanges();
            }
            return true;
        }

        public List<Sessions?> GetListSessionByAccountUuid(string accountUuid)
        {
            return _dbContext.Sessions.Where(x => x.UserUuid == accountUuid).ToList();
        }
        public Sessions? GetSessionByUuid(string token)
        {
            return _dbContext.Sessions.Where(x => x.Uuid == token).SingleOrDefault();
        }
    }
}
