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
        BaseResponse DeleteStudentSkill(string uuid);
        BaseResponseMessageItem<StudentSkillDTO> GetListStudentSkill(GetListStudentSkillByStudentUuid request);
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

        public BaseResponse DeleteStudentSkill(string uuid)
        {
            var response = new BaseResponse();
            var studentskill = _studentSkillRepository.GetDetailStudentSkill(uuid);
            if (studentskill == null ) {
            throw new ErrorException(ErrorCode.STUDENTSKILL_NOT_FOUND);
            }
            return ExecuteInTransaction(() =>
            {
                _studentSkillRepository.DeleteItem(studentskill);
                return response;
            });
        }

        public BaseResponseMessageItem<StudentSkillDTO> GetListStudentSkill(GetListStudentSkillByStudentUuid request)
        {
            var response = new BaseResponseMessageItem<StudentSkillDTO>();
            var student = _studentRepository.GetStudentInforByStudentUuid(request.studentUuid);
            if (student == null )
            {
                throw new ErrorException(ErrorCode.STUDENT_NOT_FOUND);
            }
            var studentskill =_studentSkillRepository.GetListStudentSkillByStudentUuid(request);
            if (studentskill == null )
            {
                throw new ErrorException(ErrorCode.STUDENTSKILL_NOT_FOUND);
            }
            var studentskillDTO = _mapper.Map<List<StudentSkillDTO>>(studentskill);
            response.Data = studentskillDTO;
            return response;

        }

        public BaseResponse InsertStudentSkill(UpsertStudentSkillRequest request)
        {
            if (_studentRepository.GetStudentInforByStudentUuid(request.studentUuid) == null)
            {
                throw new ErrorException(ErrorCode.STUDENT_NOT_FOUND);

            }
            if(_skillRepository.GetSkillDetailByUuid(request.skillUuid) == null)
            {
                throw new ErrorException(ErrorCode.SKILL_NOT_FOUND);
            }
            if(_studentSkillRepository.IsStudentSkillExists(request.studentUuid,request.skillUuid)!=null)
            {
                throw new ErrorException(ErrorCode.JOBSKILL_EXISTS);
            }
            var newStudentSkill = new StudentSkill
            {
                StudentUuid = request.studentUuid,
                SkillUuid = request.skillUuid,
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
