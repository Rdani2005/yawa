using AutoMapper;
using AuthService.Dtos;
using AuthService.Models;
using AuthService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{

    [ApiController]
    [Route("api/v1/auth/roles/{roleId}/permitions")]
    public class PermitionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPermitionRepo _permitionRepo;

        public PermitionController(IMapper mapper, IPermitionRepo permitionRepo)
        {
            _mapper = mapper;
            _permitionRepo = permitionRepo;
        }

        [HttpGet]
        public ActionResult<PermitionReadDto> GetAllPermitions(int roleId)
        {
            IEnumerable<Permition> permitions = _permitionRepo.GetAllPermitionsByRol(roleId);
            return Ok(_mapper.Map<IEnumerable<PermitionReadDto>>(permitions));
        }

        [HttpGet("{permitionId}", Name = "GetPermitionById")]
        public ActionResult<PermitionReadDto> GetPermitionById(int roleId, int permitionId)
        {
            Permition permition = _permitionRepo.GetPermitionById(roleId);
            return permition == null ? NotFound() : Ok(_mapper.Map<PermitionReadDto>(permition));
        }

    }
}