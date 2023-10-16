using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl, 
            otp => otp.MapFrom(src => src.Images.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<image, ImageDto>();
            CreateMap<Qa, QaDto>();
        }
    }
}