using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;

namespace apicampusjob.Repository
{
    public interface IStudentSkillRepository : IBaseRepository
    {
        List<StudentSkill> GetListStudentSkillByStudentUuid(GetListStudentSkillByStudentUuid request);
        StudentSkill GetDetailStudentSkill(string uuid);
    }
    public class StudentSkillRepository : BaseRepository, IStudentSkillRepository
    {
        public StudentSkillRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public StudentSkill GetDetailStudentSkill(string uuid)
        {
            return _dbContext.StudentSkill.FirstOrDefault(x => x.Uuid == uuid);
        }

        public List<StudentSkill> GetListStudentSkillByStudentUuid(GetListStudentSkillByStudentUuid request)
        {
            return _dbContext.StudentSkill.Where(x =>x.StudentUuid == request.Student_Uuid).ToList();
        }
    }
}
