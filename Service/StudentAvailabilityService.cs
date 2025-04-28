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
    public interface IStudentAvailabilityService
    {
        BaseResponse InsertStudentAvailability(UpsertStudentAvailability request);
        BaseResponse UpdateStudentAvailability(UpsertStudentAvailability request);
    }
    public class StudentAvailabilityService : BaseService, IStudentAvailabilityService
    {
        public IStudentAvailabilityRepository _studentAvailabilityRepository;
        public IStudentRepository _studentRepository;
        public StudentAvailabilityService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IStudentAvailabilityRepository studentAvailabilityRepository, IStudentRepository studentRepository) : base(dbContext, mapper, configuration)
        {
            _studentAvailabilityRepository = studentAvailabilityRepository;
            _studentRepository = studentRepository;
        }

        public BaseResponse InsertStudentAvailability(UpsertStudentAvailability request)
        {
            if (_studentRepository.GetStudentInforByStudentUuid(request.Student_Uuid) == null)
            {
                throw new ErrorException(ErrorCode.STUDENT_NOT_FOUND);

            }
            var newStudentAvailability = new StudentAvailability
            {
                StudentUuid = request.Student_Uuid,
                DayOfWeek = request.Day_of_week,
                StartTime = request.Start_time,
                EndTime = request.End_time,

            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<StudentAvailabilityDTO>
                {
                    Data = _mapper.Map<StudentAvailabilityDTO>(_studentAvailabilityRepository.CreateItem(newStudentAvailability))
                };
            });
        }
        public BaseResponse UpdateStudentAvailability(UpsertStudentAvailability request)
        {
            var response = new BaseResponse();
            var oldAvailability = _studentAvailabilityRepository.GetAvailabilityByUuid(request.Uuid);
            if (_studentRepository.GetStudentInforByStudentUuid(request.Student_Uuid) == null)
            {
                throw new ErrorException(ErrorCode.STUDENT_NOT_FOUND);

            }
            if (oldAvailability == null)
            {
                throw new ErrorException(ErrorCode.AVAILABLITY_NOT_FOUND);
            }
            oldAvailability.DayOfWeek = request.Day_of_week;
            oldAvailability.StartTime = request.Start_time;
            oldAvailability.EndTime = request.End_time;
            _studentAvailabilityRepository.UpdateItem(oldAvailability);
            return response;
        }
    }

 }

