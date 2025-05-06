using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace apicampusjob.Repository
{
    public interface IStudentAvailabilityRepository : IBaseRepository
    {
        List<StudentAvailability> GetAvailabilityByStudentUuid(GetAvailabilityByStudenUuid request);
        StudentAvailability GetAvailabilityByUuid(string uuid);
    }
    public class StudentAvailabilityRepository : BaseRepository, IStudentAvailabilityRepository
    {
        public StudentAvailabilityRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<StudentAvailability> GetAvailabilityByStudentUuid(GetAvailabilityByStudenUuid request)
        {
           return _dbContext.StudentAvailability
                .Where(x =>x.StudentUuid == request.studentUuid)
                .ToList();
        }

        public StudentAvailability GetAvailabilityByUuid(string uuid)
        {
            return _dbContext.StudentAvailability.FirstOrDefault(x => x.Uuid == uuid);
        }
    }
}
