using AccountService;
using AutoMapper;
using MovementService.Dtos;
using MovementService.Models;

namespace MovementService.Profiles
{
    public class MovementProfile : Profile
    {
        public MovementProfile()
        {
            CreateMap<AccountPublishedDto, Account>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ActualAmount, opt => opt.MapFrom(src => src.ActualAmount))
            ;

            CreateMap<Account, AccountReadDto>();

            CreateMap<GrpcAccountModel, Account>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.ActualAmount, opt => opt.MapFrom(src => src.ActualAmount))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.AccountId));


            CreateMap<Movement, MovementReadDto>();
            CreateMap<MovementAddDto, Movement>();

            CreateMap<MovementReadDto, MovementPublishedDto>()
                .ForMember(dest => dest.MovementId, opt => opt.MapFrom(src => src.Id))

            ;

            CreateMap<MovementType, TypeReadDto>();
            CreateMap<TypeAddDto, MovementType>();
        }
    }
}