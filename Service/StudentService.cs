using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Enums;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using apicampusjob.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace apicampusjob.Service
{
    public interface IStudentService
    {
        BaseResponse InsertStudent(UpsertStudentRequest student);
        BaseResponse UpdateStudent(UpsertStudentRequest student, TokenInfo token);
        BaseResponseMessage<StudentDTO> GetDetailStudent(string Useruuid); 
    }
    public class StudentService : BaseService, IStudentService
    {
        public IStudentRepository _studentRepository;
        public IUserRepository _userRepository;
        public StudentService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IStudentRepository studentRepository, IUserRepository userRepository) : base(dbContext, mapper, configuration)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }
        public BaseResponseMessage<StudentDTO> GetDetailStudent(string Useruuid)
        {
            var response = new BaseResponseMessage<StudentDTO>();
            var student = _studentRepository.GetStudentInforByUserUuid(Useruuid);
            if (student == null)
            {
                throw new ErrorException(ErrorCode.STUDENT_NOT_FOUND);
            }
            var detailstudentDTO = _mapper.Map<StudentDTO>(student);
            response.Data = detailstudentDTO;
            return response;
        }

        public BaseResponse InsertStudent(UpsertStudentRequest student)
        {
            var existingUser = _userRepository.GetUserByUuid(student.UserUuid);
            if (existingUser == null)
            {
                throw new ErrorException(ErrorCode.USER_NOT_FOUND);
            }
            if (_studentRepository.GetStudentInforByUserUuid(student.UserUuid) != null)
            {
                throw new ErrorException(ErrorCode.STUDENT_ALREADY_EXISTS);
            }
            var newStudent = new Student
            {
                UserUuid = student.UserUuid,
                Fullname = student.Fullname,
                PhoneNumber = student.PhoneNumber,
                Gender = student.Gender,
                Birthday = student.Birthday,
                University = student.University,
                Major = student.Major,
                Matp = student.Matp,
                Maqh = student.Maqh,
                Xaid = student.Xaid,
            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<StudentDTO>
                {
                    Data = _mapper.Map<StudentDTO>(_studentRepository.CreateItem(newStudent))
                };
            });
           
        }

        public BaseResponse UpdateStudent(UpsertStudentRequest student, TokenInfo token)
        {
            var response = new BaseResponse();
            var oldStudent = _studentRepository.GetStudentInforByUserUuid(student.UserUuid);
            if (oldStudent == null)
            {
                throw new ErrorException(ErrorCode.STUDENT_NOT_FOUND);
            }
            oldStudent.Fullname = student.Fullname;
            oldStudent.Gender = student.Gender;
            oldStudent.PhoneNumber = student.PhoneNumber;
            oldStudent.Birthday = student.Birthday;
            oldStudent.University = student.University;
            oldStudent.Major = student.Major;
            oldStudent.Matp = student.Matp;
            oldStudent.Maqh = student.Maqh;
            oldStudent.Xaid = student.Xaid;
            _studentRepository.UpdateItem(student);
            return response;
        }
    }
}
