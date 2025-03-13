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
        }


    }
}