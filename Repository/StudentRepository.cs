using apicampusjob.Databases.TM;
using Microsoft.EntityFrameworkCore;
namespace apicampusjob.Repository
{
    public interface IStudentRepository : IBaseRepository
    {
        Student GetStudentInforByUserUuid (string uuid);
    }
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(DBContext dbcontext):base(dbcontext) { }
        public Student GetStudentInforByUserUuid(string UserUuid)
        {
            return _dbContext.Student.Include(x => x.MatpNavigation).Include(x => x.Xa).Include(x => x.MaqhNavigation).FirstOrDefault(x => x.UserUuid == UserUuid);
        }
    }
}
