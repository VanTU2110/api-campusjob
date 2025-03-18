using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace apicampusjob.Repository
{
    public interface IJobScheduleRepository : IBaseRepository
    {
        List<JobSchedule> GetPageListJobSchedule(GetPageListSchedule request);
        int Count(GetPageListSchedule request);
        List <JobSchedule> GetJobScheduleByJobUuid(GetListJobScheduleByJobUuid request);
        JobSchedule GetJobScheduleByJobUuid(string uuid);
    }
    public class JobScheduleRepository : BaseRepository, IJobScheduleRepository
    {
        public JobScheduleRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public int Count(GetPageListSchedule request)
        {
            return GetPageListJobSchedule(request).Count();   
        }

        public List<JobSchedule> GetJobScheduleByJobUuid(GetListJobScheduleByJobUuid request)
        {
            return _dbContext.JobSchedule
           .Include(x => x.JobUu) // Nếu cần lấy thông tin liên quan của Job
           .Where(x => x.JobUuid == request.JobUuid)
           .ToList();
        }

        public JobSchedule GetJobScheduleByJobUuid(string uuid)
        {
            return _dbContext.JobSchedule.FirstOrDefault(x => x.Uuid == uuid);
        }

        public List<JobSchedule> GetPageListJobSchedule(GetPageListSchedule request)
        {
            return _dbContext.JobSchedule
                .Include(x=> x.JobUu)
                .ThenInclude(x =>x.CompanyUu)
                .Where(x=>string.IsNullOrEmpty(request.Day_of_week) ||x.DayOfWeek.Contains(request.Day_of_week))
                .Where(x =>
                (!request.Start_Time.HasValue || x.StartTime >= request.Start_Time) &&
                (!request.End_Time.HasValue || x.EndTime <= request.End_Time))
        .ToList();
        }
    }
}
