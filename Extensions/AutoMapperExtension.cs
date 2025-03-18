using AutoMapper;
using apicampusjob.Databases.TM;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Utils;

namespace apicampusjob.Extensions
{
    public class AutoMapperExtension : Profile
    {
        public AutoMapperExtension()
        {
            CreateMap<User, UserDTO>();
            CreateMap<RegisterAccountRequest, User>();
          
            CreateMap<DevvnQuanhuyen, InfoCatalogDTO>()
                .ForMember(destination => destination.Name,
                options => options.MapFrom(source => source.Name))
                 .ForMember(destination => destination.Code,
                options => options.MapFrom(source => source.Maqh)) ;

            CreateMap<DevvnTinhthanhpho, InfoCatalogDTO>()
                .ForMember(destination => destination.Name,
                options => options.MapFrom(source => source.Name))
                 .ForMember(destination => destination.Code,
                options => options.MapFrom(source => source.Matp));

            CreateMap<DevvnXaphuongthitran, InfoCatalogDTO>()
                .ForMember(destination => destination.Name,
                options => options.MapFrom(source => source.Name))
                 .ForMember(destination => destination.Code,
                options => options.MapFrom(source => source.Xaid));
            CreateMap<Student, StudentDTO>().ForMember(destination => destination.TP,
                options => options.MapFrom(source => source.MatpNavigation))
                    .ForMember(destination => destination.QH,
                options => options.MapFrom(source => source.MaqhNavigation))
                    .ForMember(destination => destination.Xa,
                options => options.MapFrom(source => source.Xa)); 

            CreateMap<UpsertStudentRequest, StudentDTO>();
            CreateMap<UpsertCompaniesRequest,CompaniesDTO>();
            CreateMap<Companies, CompaniesDTO>().ForMember(destination => destination.TP,
                options => options.MapFrom(source => source.MatpNavigation))
                    .ForMember(destination => destination.QH,
                options => options.MapFrom(source => source.MaqhNavigation))
                    .ForMember(destination => destination.Xa,
                options => options.MapFrom(source => source.Xa)); ;


   
            CreateMap<DevvnTinhthanhpho, CategoryAddressDTO>()
                .ForMember(destinaton => destinaton.Name, options => options.MapFrom(source => source.Name))
                 .ForMember(destinaton => destinaton.Code, options => options.MapFrom(source => source.Matp));
            CreateMap<DevvnQuanhuyen, CategoryAddressDTO>()
                .ForMember(destinaton => destinaton.Name, options => options.MapFrom(source => source.Name))
                 .ForMember(destinaton => destinaton.Code, options => options.MapFrom(source => source.Maqh));
            CreateMap<DevvnXaphuongthitran, CategoryAddressDTO>()
                .ForMember(destinaton => destinaton.Name, options => options.MapFrom(source => source.Name))
                 .ForMember(destinaton => destinaton.Code, options => options.MapFrom(source => source.Xaid));

            CreateMap<Job, JobDTO>()
                .ForMember(destination =>destination.Company,options => options.MapFrom(source => source.CompanyUu))
                .ForMember(destination => destination.Schedule, options => options.MapFrom(source => source.JobSchedule));

            CreateMap<UpsertJobRequest, JobDTO>();

            CreateMap<Companies, InfoCatalogDTO>()
                .ForMember(destination => destination.Uuid, options => options.MapFrom(source => source.Uuid))
                .ForMember(destination => destination.Name, options => options.MapFrom(source => source.Name));
            CreateMap<JobSchedule, JobScheduleDTO>()
                 .ForMember(destination => destination.Uuid, options => options.MapFrom(source => source.Uuid))
                .ForMember(destination => destination.JobUuid, options => options.MapFrom(source => source.JobUuid))
                .ForMember(destination => destination.DayOfWeek, options => options.MapFrom(source => source.DayOfWeek))
                .ForMember(destination => destination.StartTime, options => options.MapFrom(source => source.StartTime))
                .ForMember(destination =>destination.EndTime, options => options.MapFrom(source => source.EndTime));


            CreateMap<UpsertScheduleRequest, ScheduleInfoCatalogDTO>();

            CreateMap<JobSchedule, ScheduleInfoCatalogDTO>()
                .ForMember(destination => destination.Job, options => options.MapFrom(source => source.JobUu));
            CreateMap<Job, JobInfoCatalogDTO>().ForMember(destination => destination.Company, options => options.MapFrom(source => source.CompanyUu));

        }


    }
}