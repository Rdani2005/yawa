using AccountService.Models;
using AccountService.Repositories;
using AutoMapper;
using Grpc.Core;

namespace AccountService.SyncDataServices.Grpc
{
    public class GrpcAccountService : GrpcAccount.GrpcAccountBase
    {
        private readonly IAccountRepo _repo;
        private readonly IMapper _mapper;

        public GrpcAccountService(IAccountRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public override Task<AccountResponse> GetAllAccounts(
            GetAllAccountsRequest request,
            ServerCallContext context
        )
        {
            var res = new AccountResponse();
            IEnumerable<Account> accounts = _repo.GetAllAccounts();
            foreach (Account account in accounts)
            {
                res.Account.Add(
                    _mapper.Map<GrpcAccountModel>(account)
                );
            }

            return Task.FromResult(res);
        }
    }
}