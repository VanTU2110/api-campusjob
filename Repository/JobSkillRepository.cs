﻿using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;

namespace apicampusjob.Repository
{
    public interface IJobSkillRepository : IBaseRepository
    {
        List<JobSkill> GetListJobSkillByJobUuid(string jobUuid);
        JobSkill GetDetailJobSkill(string uuid);
        JobSkill IsJobSkillExists(string jobUuid, string skillUuid);
    }

    public class JobSkillRepository : BaseRepository, IJobSkillRepository
    {
        public JobSkillRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<JobSkill> GetListJobSkillByJobUuid(string jobUuid)
        {
            return _dbContext.JobSkill
                .Where(x => x.JobUuid == jobUuid)
                .ToList();
        }

        public JobSkill GetDetailJobSkill(string uuid)
        {
            return _dbContext.JobSkill
                .FirstOrDefault(x => x.Uuid == uuid);
        }

        public JobSkill IsJobSkillExists(string jobUuid, string skillUuid)
        {
            return _dbContext.JobSkill.FirstOrDefault(js => js.JobUuid == jobUuid && js.SkillUuid == skillUuid);
        }
    }
}
