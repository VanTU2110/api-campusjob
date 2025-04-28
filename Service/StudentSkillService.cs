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
    public interface IStudentSkillService
    {
        BaseResponse InsertStudentSkill(UpsertStudentSkillRequest request);
    }
    public class StudentSkillService : BaseService, IStudentSkillService
    {
        public IStudentRepository _studentRepository ;
        public IStudentSkillRepository _studentSkillRepository;
        public ISkillRepository _skillRepository ;
        public StudentSkillService(DBContext dbContext, IMapper mapper, IConfiguration configuration,IStudentRepository studentRepository,IStudentSkillRepository studentSkillRepository,ISkillRepository skillRepository ): base(dbContext, mapper, configuration)
        {
            _studentRepository = studentRepository;
            _studentSkillRepository = studentSkillRepository;
            _skillRepository = skillRepository;
        }

        public BaseResponse InsertStudentSkill(UpsertStudentSkillRequest request)
        {
            if (_studentRepository.GetStudentInforByStudentUuid(request.Student_Uuid) == null)
            {
                throw new ErrorException(ErrorCode.STUDENT_NOT_FOUND);

            }
            if(_skillRepository.GetSkillDetailByUuid(request.Skill_Uuid) == null)
            {
                throw new ErrorException(ErrorCode.SKILL_NOT_FOUND);
            }
            var newStudentSkill = new StudentSkill
            {
                StudentUuid = request.Student_Uuid,
                SkillUuid = request.Skill_Uuid,
                Proficiency = request.Proficiency,
            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<StudentSkillDTO>
                {
                    Data = _mapper.Map<StudentSkillDTO>(_studentSkillRepository.CreateItem(newStudentSkill))
                };
            });
        }
    }
}
