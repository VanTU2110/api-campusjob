using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace apicampusjob.Repository
{
    public interface ICompaniesRepository : IBaseRepository
    {
        Companies GetCompaniesInforByUserUuid(string UserUuid);
        Companies GetCompaniesInforbyUuid (string uuid);
        List<Companies> GetPageListCompanies(GetPageListCompany request);
    }
    public class CompaniesRepository : BaseRepository, ICompaniesRepository
    {
        public CompaniesRepository(DBContext dbcontext) : base(dbcontext) { }
        public Companies GetCompaniesInforByUserUuid(string UserUuid)
        {
            return _dbContext.Companies
                .Include(x => x.MatpNavigation)
                .Include(x => x.Xa)
                .Include(x => x.MaqhNavigation).FirstOrDefault(x => x.UserUuid == UserUuid);
        }

        public Companies GetCompaniesInforbyUuid(string uuid)
        {
            return _dbContext.Companies
                .Include(x => x.MatpNavigation)
                .Include(x => x.Xa)
                .Include(x => x.MaqhNavigation).FirstOrDefault(x => x.Uuid == uuid);
        }

        public List<Companies> GetPageListCompanies(GetPageListCompany request)
        {
            return _dbContext.Companies
               .Include(x => x.MatpNavigation)
               .Include(x => x.Xa)
               .Include(x => x.MaqhNavigation)
               .Where(x => string.IsNullOrEmpty(request.Keyword) || x.Name.Contains(request.Keyword)).ToList();
        }
    }
}
