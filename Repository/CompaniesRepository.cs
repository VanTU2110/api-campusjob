using apicampusjob.Databases.TM;
using Microsoft.EntityFrameworkCore;

namespace apicampusjob.Repository
{
    public interface ICompaniesRepository : IBaseRepository
    {
        Companies GetCompaniesInforByUserUuid(string uuid);
    }
    public class CompaniesRepository : BaseRepository, ICompaniesRepository
    {
        public CompaniesRepository(DBContext dbcontext) : base(dbcontext) { }
        public Companies GetCompaniesInforByUserUuid(string UserUuid)
        {
            return _dbContext.Companies.Include(x => x.MatpNavigation).Include(x => x.Xa).Include(x => x.MaqhNavigation).FirstOrDefault(x => x.UserUuid == UserUuid);
        }
    }
}
