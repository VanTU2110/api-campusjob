using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;
using Microsoft.EntityFrameworkCore;
namespace apicampusjob.Repository
{
    public interface IStudentRepository : IBaseRepository
    {
        Student GetStudentInforByUserUuid (string uuid);
        Student GetStudentInforByStudentUuid(string StudentUuid);
        List<Student> GetPageListStudet(GetPageListStudent request);
        List<Student> GetSuggestedStudentsForJob(SuggestStudentsForJob request);


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

        public List<Student> GetPageListStudet(GetPageListStudent request)
        {
            return _dbContext.Student
               .Include(x => x.MatpNavigation)
               .Include(x => x.Xa)
               .Include(x => x.MaqhNavigation)
               .Where(x => string.IsNullOrEmpty(request.Keyword) || x.Fullname.Contains(request.Keyword))
               .ToList();
        }
        public List<Student> GetSuggestedStudentsForJob(SuggestStudentsForJob request)
        {
            var job = _dbContext.Job
                .Include(j => j.JobSkill).ThenInclude(js => js.SkillUu)
                .Include(j => j.JobSchedule)
                .FirstOrDefault(j => j.Uuid == request.jobUuid);

            if (job == null) return [];

            var requiredSkillIds = job.JobSkill.Select(js => js.SkillUuid).ToList();
            var requiredSchedules = job.JobSchedule.Select(s => new { s.DayOfWeek, s.StartTime, s.EndTime }).ToList();

            return _dbContext.Student
                .Include(s => s.StudentSkill).ThenInclude(ss => ss.SkillUu)
                .Include(s => s.StudentAvailability)
                .Include(x => x.MatpNavigation)
                .Include(x => x.MaqhNavigation)
                .Include(x => x.Xa)
                .Include(x =>x.UserUu)
                .AsEnumerable()
                .Where(student =>
                    student.StudentSkill.Any(ss => requiredSkillIds.Contains(ss.SkillUuid)) || // ít nhất 1 kỹ năng khớp
                    student.StudentAvailability.Any(sa => requiredSchedules.Any(rs =>
                        sa.DayOfWeek == rs.DayOfWeek &&
                        sa.StartTime <= rs.StartTime &&
                        sa.EndTime >= rs.EndTime
                    ))
                )
                .ToList();
        }


    }
}
