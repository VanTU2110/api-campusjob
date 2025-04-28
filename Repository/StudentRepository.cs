using apicampusjob.Databases.TM;
using Microsoft.EntityFrameworkCore;
namespace apicampusjob.Repository
{
    public interface IStudentRepository : IBaseRepository
    {
        Student GetStudentInforByUserUuid (string uuid);
        Student GetStudentInforByStudentUuid(string StudentUuid);
    }
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(DBContext dbcontext):base(dbcontext) { }
        public Student GetStudentInforByUserUuid(string UserUuid)
        {
            return _dbContext.Student
                .Include(x => x.MatpNavigation)
                .Include(x => x.Xa)
                .Include(x => x.MaqhNavigation)
                .Include(x => x.StudentAvailability)
                .Include(x =>x.StudentSkill)
                .ThenInclude(ss => ss.SkillUu)
                .FirstOrDefault(x => x.UserUuid == UserUuid);
        }

        public Student GetStudentInforByStudentUuid( string StudentUuid)
        {
            return _dbContext.Student
                .Include(x => x.MatpNavigation)
                .Include(x => x.Xa)
                .Include(x => x.MaqhNavigation)
                .Include(x => x.StudentAvailability)
                .Include(x =>x.StudentSkill)
                .ThenInclude(ss => ss.SkillUu)
                .FirstOrDefault(x => x.Uuid == StudentUuid);

        }
    }
}
