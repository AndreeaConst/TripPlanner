using AutoMapper;
using TripPlannerBackend.Models.Domains;
using TripPlannerBackend.Models.DTOs;

namespace TripPlannerBackend.Mapping
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            CreateMap<UserAccount, UserAccountDto>()
                .ForMember(dest => dest.AccessToken, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore());
        }
    }
}
