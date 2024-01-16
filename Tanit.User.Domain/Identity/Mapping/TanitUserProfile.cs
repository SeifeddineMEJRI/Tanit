using AutoMapper;
using Tanit.User.Domain.Identity.Model;
using Tanit.User.Domain.Identity.Request;

namespace Tanit.User.Domain.Identity.Mapping
{
    public class TanitUserProfile : Profile
    {
        public TanitUserProfile() => CreateMap<UserRegistrationRequest, TanitUser>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true));
    }
}
