using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;

namespace apicampusjob.Repository
{
    public interface ISkillRepository :IBaseRepository
    {
        List<Skills>GetPageListSkill(GetPageListSkillRequest request);
        int Count(GetPageListSkillRequest request);
        Skills GetSkillDetailByUuid(string uuid);
        bool IsSkillNameExisted(string name);
    }
    public class SkillRepository : BaseRepository, ISkillRepository
    {
        public SkillRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public int Count(GetPageListSkillRequest request)
        {
            return GetPageListSkill(request).Count;
        }

        public List<Skills> GetPageListSkill(GetPageListSkillRequest request)
        {
            return _dbContext.Skills.Where(x =>string.IsNullOrEmpty(request.Keyword) ||x.Name.Contains(request.Keyword)).ToList();
        }

        public Skills GetSkillDetailByUuid(string uuid)
        {
            return _dbContext.Skills.FirstOrDefault(x => x.Uuid == uuid);
        }
        // Trong SkillRepository
        public bool IsSkillNameExisted(string name)
        {
            return _dbContext.Skills.Any(s => s.Name.ToLower().Trim() == name.ToLower().Trim());
        }

    }
}
