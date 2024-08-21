using AutoMapper;
using CVManagementAPI.DTO;
using CVManagementAPI.Models;
using CVManagementAPI.Request;

namespace CVManagementAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {

            CreateMap<AddCVRequest, CV>().ReverseMap();
            CreateMap<PutCVRequest, CvDTO>().ReverseMap();
            CreateMap<CV, CvDTO>().ReverseMap();
            CreateMap<Position, PositionDTO>().ReverseMap();
            CreateMap<Source, SourceDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<ColumnLayout, ColumnLayoutDTO>().ReverseMap();
            CreateMap<EmailMessage, EmailMessageDTO>().ReverseMap();
            CreateMap<ColumnRelationship, ColumnRelationshipDTO>().ReverseMap();
            CreateMap<AppUser, AccountDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<EditAccountRequest, AppUser>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
        }
    }
}
