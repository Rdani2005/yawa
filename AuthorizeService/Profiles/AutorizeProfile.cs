using AutoMapper;
using AuthorizeService.Dtos;
using AuthorizeService.Models;
using AuthorizeService;

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
            CreateMap<PermitionReadDto, PermitionPublishedDto>();
            CreateMap<RolReadDto, RolPublishedDto>()
                .ForMember(
                    r => r.Permitions, opt => opt.MapFrom(src => src.Permitions)
                );

            CreateMap<Permition, GrpcPermitionModel>()
                .ForMember(dest => dest.PermitionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


            CreateMap<Rol, GrpcRolModel>()
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(
                    r => r.Permitions, opt => opt.MapFrom(src => src.PermitionRols.Select(pr => pr.Permition))
                );


        }
    }
}