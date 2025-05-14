using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Enums;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using AutoMapper;

namespace apicampusjob.Service
{
    public interface IJobSkillService
    {
        BaseResponse InsertJobSkill(UpsertJobSkill request);
        BaseResponse DeleteJobSKill(string uuid);
    }
    public class JobSkillService : BaseService, IJobSkillService
    {
        public IJobRepository _jobrepositpry;
        public ISkillRepository _skillRepository;
        public IJobSkillRepository _jobSkillRepository;
        public JobSkillService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IJobSkillRepository jobSkillRepository,IJobRepository jobRepository, ISkillRepository skillRepository) : base(dbContext, mapper, configuration)
        {
            _jobSkillRepository = jobSkillRepository;
            _skillRepository = skillRepository;
            _jobrepositpry = jobRepository;
        }

        public BaseResponse DeleteJobSKill(string uuid)
        {
            var response = new BaseResponse();
            var jobskill = _jobSkillRepository.GetDetailJobSkill(uuid);
            if (jobskill == null) 
            {
                throw new ErrorException(ErrorCode.JOBSKILL_NOT_FOUND);

            }
            return ExecuteInTransaction(() =>
            {
                _jobSkillRepository.DeleteItem(jobskill);
                return response;
            });
        }

        public BaseResponse InsertJobSkill(UpsertJobSkill request)
        {
            if(_jobrepositpry.GetJobByUuid(request.JobUuid) == null)
            {
                throw new ErrorException(ErrorCode.REPORT_NOT_FOUND);
            }
            if (_skillRepository.GetSkillDetailByUuid(request.SkillUuid) == null)
            {
                throw new ErrorException(ErrorCode.SKILL_NOT_FOUND);
            }
            var newJobSkill = new JobSkill
            {
                JobUuid = request.JobUuid,
                SkillUuid = request.SkillUuid,
            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<JobSkillDTO>
                {
                    Data = _mapper.Map<JobSkillDTO>(_jobSkillRepository.CreateItem(newJobSkill)),
                };
            });
        }
    }
}
