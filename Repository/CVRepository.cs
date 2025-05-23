using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;

namespace apicampusjob.Repository
{
    public interface ICVRepository :IBaseRepository
    {
        public List<StudentCv> GetCVByStudentUuid(GetCVByStudenUuidRequest request);
        public StudentCv GetDetailCV(string uuid);
    }
    public class CVRepository : BaseRepository, ICVRepository
    {
        public CVRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<StudentCv> GetCVByStudentUuid(GetCVByStudenUuidRequest request)
        {
            return _dbContext.StudentCv.Where(x =>x.StudentUuid == request.studentUuid).ToList();
        }

        public StudentCv GetDetailCV(string uuid)
        {
            return _dbContext.StudentCv.FirstOrDefault(x => x.Uuid == uuid);
        }
    }
}
