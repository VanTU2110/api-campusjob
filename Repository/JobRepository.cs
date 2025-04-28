using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace apicampusjob.Repository
{
    public interface IJobRepository : IBaseRepository
    {
       List<Job> GetPageListJob(GetPageListJobRequest request);
        int Count(GetPageListJobRequest job);
        Job GetJobByUuid (string uuid);
    }
    public class JobRepository : BaseRepository, IJobRepository
    {
        public JobRepository(DBContext dbContext) : base(dbContext) { }
        public int Count(GetPageListJobRequest request)
        {
           return GetPageListJob(request).Count();
        }

        public Job GetJobByUuid(string uuid)
        {
            return _dbContext.Job
                .Include(x => x.CompanyUu)
                 .Include(x => x.JobSchedule)
                 .Include(x =>x.JobSkill)
                 .ThenInclude(x=>x.SkillUu)
                 .FirstOrDefault(x=>x.Uuid == uuid);

        }

        public List<Job> GetPageListJob(GetPageListJobRequest request)
        {
            return _dbContext.Job
                 .Include(x => x.CompanyUu)
                 .Include(x => x.JobSchedule)
                 .Include(x => x.JobSkill)
                 .ThenInclude(x => x.SkillUu)
                 .Where(x =>
                    (request.SalaryMin == null || x.SalaryMin >= request.SalaryMin) &&
                    (request.SalaryMax == null || x.SalaryMax <= request.SalaryMax) &&
                    (request.SalaryFixed == null || x.SalaryFixed >= request.SalaryFixed))
                 .Where(x => string.IsNullOrEmpty(request.Keyword) || x.Title.Contains(request.Keyword))
                 .Where(x => string.IsNullOrEmpty(request.CompanyUuid) || x.CompanyUuid == request.CompanyUuid)
                 .Where(x => string.IsNullOrEmpty(request.SalaryType) || x.SalaryType == request.SalaryType)
                 .Where(x=>string.IsNullOrEmpty(request.JobType) || x.JobType == request.JobType).ToList();
        }
    }
}
