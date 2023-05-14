using AuthService.Dtos;
using AuthService.Models;
using AutoMapper;

namespace AuthService.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Permition, PermitionReadDto>();
            CreateMap<Rol, RolReadDto>()
                .ForMember(
                    r => r.Permitions, opt => opt.MapFrom(src => src.PermitionRols.Select(pr => pr.Permition))
                );
            CreateMap<PermitionReadDto, PermitionPublishedDto>();
            CreateMap<PermitionAddDto, Permition>();


            CreateMap<UserReadDto, UserPublishedDto>();

            CreateMap<RolAddDto, Rol>();

            CreateMap<User, UserReadDto>()
                .ForMember(
                    u => u.UserRole, opt => opt.MapFrom(src => src.UserRole)
                )
            ;

            CreateMap<AddUserDto, User>();

            CreateMap<User, GrpcUserModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Identification, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))

            ;

            // CreateMap<Permition, GrpcPermitionModel>()
            //     .ForMember(dest => dest.PermitionId, opt => opt.MapFrom(src => src.Id))
            //     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


            // CreateMap<Rol, GrpcRolModel>()
            //     .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Id))
            //     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //     .ForMember(
            //         r => r.Permitions, opt => opt.MapFrom(src => src.PermitionRols.Select(pr => pr.Permition))
            //     );
        }
    }
}