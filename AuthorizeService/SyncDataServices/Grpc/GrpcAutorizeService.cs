using AuthorizeService.Models;
using AuthorizeService.Repositories;
using AutoMapper;
using Grpc.Core;

namespace AuthorizeService.SyncDataServices.Grpc
{
    public class GrpcAutorizeService : GrpcRol.GrpcRolBase
    {
        private readonly IMapper _mapper;
        private readonly IRolRepo _rolRepo;
        private readonly IPermitionRepo _permitionRepo;

        public GrpcAutorizeService(
            IMapper mapper,
            IRolRepo rolRepo,
            IPermitionRepo permitionRepo)
        {
            _mapper = mapper;
            _rolRepo = rolRepo;
            _permitionRepo = permitionRepo;
        }


        public override Task<RolResponse> GetAllRols(
            GetAllRequest request,
            ServerCallContext context
        )
        {
            var response = new RolResponse();
            IEnumerable<Rol> rols = _rolRepo.GetAllRols();

            foreach (Rol rol in rols)
            {
                var permitions =
                _mapper.Map<IEnumerable<GrpcPermitionModel>>(
                    _permitionRepo.GetAllPermitionsByRol(rol.Id));
                var mappedRole = _mapper.Map<GrpcRolModel>(rol);
                // mappedRole.Permitions = permitions;
                response.Rol.Add(mappedRole);
            }

            return Task.FromResult(response);
        }
    }
}