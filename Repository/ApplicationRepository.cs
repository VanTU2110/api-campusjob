using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace apicampusjob.Repository
{
    public interface IApplicationRepository : IBaseRepository
    {
        List<Applications> GetPageListApplyByJobUuid(GetPageListApplyByJobUuid request);
        List<Applications> GetPageListApplyByStudentUuid(GetPageListApplyByStudentUuid request);
        Applications GetApplicationsByUuid(string uuid);
        int CountApplyByJobUuid(GetPageListApplyByJobUuid request);
        int CountApplyByStudentUuid(GetPageListApplyByStudentUuid request);
        Applications? CheckStudentApply(string studentUuid, string jobUuid);
    }
    public class ApplicationRepository : BaseRepository, IApplicationRepository
    {
        public ApplicationRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public Applications? CheckStudentApply(string studentUuid, string jobUuid)
        {
            return _dbContext.Applications.FirstOrDefault(x => x.StudentUuid == studentUuid && x.JobUuid == jobUuid);
        }

        public int CountApplyByJobUuid(GetPageListApplyByJobUuid request)
        {
           return GetPageListApplyByJobUuid(request).Count();
        }

        public int CountApplyByStudentUuid(GetPageListApplyByStudentUuid request)
        {
            return GetPageListApplyByStudentUuid(request).Count();
        }

        public Applications GetApplicationsByUuid(string uuid)
        {
            return _dbContext.Applications
               .FirstOrDefault(x => x.Uuid == uuid);
        }

        public List<Applications> GetPageListApplyByJobUuid(GetPageListApplyByJobUuid request)
        {
            return _dbContext.Applications
                .Where(x =>x.JobUuid == request.JobUuid).ToList();
        }

        public List<Applications> GetPageListApplyByStudentUuid(GetPageListApplyByStudentUuid request)
        {
            return _dbContext.Applications
                .Where(x => x.StudentUuid == request.StudentUuid).ToList();
        }
    }
}
