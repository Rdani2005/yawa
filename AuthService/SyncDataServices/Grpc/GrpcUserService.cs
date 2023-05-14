using AuthService.Models;
using AuthService.Repositories;
using AutoMapper;
using Grpc.Core;

namespace AuthService.SyncDataServices.Grpc
{
    public class GrpcUserService : GrpcUser.GrpcUserBase
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;

        public GrpcUserService(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public override Task<UserResponse> GetAllPUsers(
            GetAllRequest request,
            ServerCallContext context
        )
        {
            var response = new UserResponse();
            IEnumerable<User> users = _repo.GetAllUsers();

            foreach (User user in users)
            {
                response.User.Add(
                    _mapper.Map<GrpcUserModel>(user)
                );
            }

            return Task.FromResult(response);
        }
    }
}