using AutoMapper;
using AuthorizeService.Dtos;
using AuthorizeService.Models;

namespace AuthorizeService.Profiles
{
    public class AutorizeProfile : Profile
    {
        public AutorizeProfile()
        {
            CreateMap<Permition, PermitionReadDto>();
            CreateMap<Rol, RolReadDto>()
                .ForMember(
                    r => r.Permitions, opt => opt.MapFrom(src => src.PermitionRols.Select(pr => pr.Permition))
                );

        }
    }
}