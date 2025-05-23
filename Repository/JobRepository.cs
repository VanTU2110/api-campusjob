using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;

namespace apicampusjob.Repository
{
    public interface IJobRepository : IBaseRepository
    {
       List<Job> GetPageListJob(GetPageListJobRequest request);
        int Count(GetPageListJobRequest job);
        Job GetJobByUuid (string uuid);
        List<Job> GetJobBySkill(SearchJobBySkillRequest request);
        List<Job> GetJobBySchedule (GetJobsByScheduleRequest request);
        Job IsJobTitleExists(string companyUuid, string jobTitle);
    }
    public class JobRepository : BaseRepository, IJobRepository
    {
        public JobRepository(DBContext dbContext) : base(dbContext) { }
        public int Count(GetPageListJobRequest request)
        {
           return GetPageListJob(request).Count();
        }

        public List<Job> GetJobBySchedule(GetJobsByScheduleRequest request)
        {
            IQueryable<Job> query = _dbContext.Job
             .Include(x => x.CompanyUu)
              .Include(x => x.JobSchedule)
            .Include(x => x.JobSkill)
              .ThenInclude(x => x.SkillUu);
            query = query.Where(job => job.JobSchedule.Any(js => js.DayOfWeek == request.dayOfWeek));

            // Nếu có chỉ định thời gian bắt đầu
            if (request.startTime.HasValue)
            {
                query = query.Where(job => job.JobSchedule.Any(
                    js => js.DayOfWeek == request.dayOfWeek && js.StartTime <= request.startTime.Value));
            }
            // Nếu có chỉ định thời gian kết thúc
            if (request.endTime.HasValue)
            {
                query = query.Where(job => job.JobSchedule.Any(
                    js => js.DayOfWeek == request.dayOfWeek && js.EndTime >= request.endTime.Value));
            }
            return query.ToList();
        
        }

        public List<Job> GetJobBySkill(SearchJobBySkillRequest request)
        {
            return _dbContext.Job
                .Include(x => x.CompanyUu)
                 .Include(x => x.JobSchedule)
                 .Include(x => x.JobSkill)
                 .ThenInclude(x => x.SkillUu)
               .Where(job => job.JobSkill.Any(js => js.SkillUuid == request.skillUuid))
                .ToList();
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

        public Job IsJobTitleExists(string companyUuid, string jobTitle)
        {
            return _dbContext.Job.FirstOrDefault(x => x.CompanyUuid == companyUuid && x.Title == jobTitle);
        }
    }
}
