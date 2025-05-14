using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Enums;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using AutoMapper;
using Microsoft.AspNet.SignalR;

namespace apicampusjob.Service
{
    public interface IStudentAvailabilityService
    {
        BaseResponse InsertStudentAvailability(UpsertStudentAvailability request);
        BaseResponse UpdateStudentAvailability(UpsertStudentAvailability request);
        BaseResponseMessageItem<StudentAvailabilityDTO> GetListAvailability(GetAvailabilityByStudenUuid request);
        BaseResponse DeleteStudentAvailability(string uuid);
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

        public BaseResponse DeleteStudentAvailability(string uuid)
        {
            var response = new BaseResponse();
            var avaibility = _studentAvailabilityRepository.GetAvailabilityByUuid(uuid);
            if (avaibility == null)
            {
                throw new ErrorException(ErrorCode.AVAILABLITY_NOT_FOUND);
            }
            return ExecuteInTransaction(() =>
            {
                _studentAvailabilityRepository.DeleteItem(avaibility);
                return response;
            });
        }

        public BaseResponseMessageItem<StudentAvailabilityDTO> GetListAvailability(GetAvailabilityByStudenUuid request)
        {
            var respone = new BaseResponseMessageItem<StudentAvailabilityDTO>();
            var lstAvailability = _studentAvailabilityRepository.GetAvailabilityByStudentUuid(request);

            if (lstAvailability != null)
            {
                var lstAvailabilityDTO = _mapper.Map<List<StudentAvailabilityDTO>>(lstAvailability);

                respone.Data = lstAvailabilityDTO;
            }
            return respone;
        }

        public BaseResponse InsertStudentAvailability(UpsertStudentAvailability request)
        {
            if (_studentRepository.GetStudentInforByStudentUuid(request.studentUuid) == null)
            {
                throw new ErrorException(ErrorCode.STUDENT_NOT_FOUND);

            }
            var newStudentAvailability = new StudentAvailability
            {
                StudentUuid = request.studentUuid,
                DayOfWeek = request.dayOfWeek,
                StartTime = request.startTime,
                EndTime = request.endTime,

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
            if (_studentRepository.GetStudentInforByStudentUuid(request.studentUuid) == null)
            {
                throw new ErrorException(ErrorCode.STUDENT_NOT_FOUND);

            }
            if (oldAvailability == null)
            {
                throw new ErrorException(ErrorCode.AVAILABLITY_NOT_FOUND);
            }
            oldAvailability.DayOfWeek = request.dayOfWeek;
            oldAvailability.StartTime = request.startTime;
            oldAvailability.EndTime = request.endTime;
            _studentAvailabilityRepository.UpdateItem(oldAvailability);
            return response;
        }
    }

 }

