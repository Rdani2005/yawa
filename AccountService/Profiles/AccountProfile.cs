using AccountService.Dtos;
using AccountService.Models;
using AuthService;
using AutoMapper;

namespace AccountService.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<UserPublishedDto, User>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id))
            ;

            CreateMap<GrpcUserModel, User>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.identification, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.Accounts, opt => opt.Ignore())
            ;

            CreateMap<User, UserReadDto>();
            CreateMap<CoinType, CoinReadDto>();
            CreateMap<Account, AccountReadDto>();
            CreateMap<AccountType, TypeReadDto>();
            CreateMap<AccountAddDto, Account>();
            CreateMap<AccountReadDto, AccountPublishedDto>();

            CreateMap<Account, GrpcAccountModel>()
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ActualAmount, opt => opt.MapFrom(src => src.ActualAmount))
                .ForMember(dest => dest.InitialAmount, opt => opt.MapFrom(src => src.InitialAmount))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                ;

        }
    }
}