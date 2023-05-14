using AutoMapper;
using AuthService.Dtos;
using AuthService.Repositories;
using Microsoft.AspNetCore.Mvc;
using AuthService.Models;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/v1/auth/roles")]
    public class RolController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRolRepo _repo;
        private readonly IPermitionRepo _permitionRepo;

        public RolController(
            IMapper mapper,
            IRolRepo repo,
            IPermitionRepo permitionRepo
        )
        {
            _mapper = mapper;
            _repo = repo;
            _permitionRepo = permitionRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RolReadDto>> GetAllRols()
        {
            var rols = _repo.GetAllRols();


            IEnumerable<RolReadDto> rolReadList = _mapper.Map<IEnumerable<RolReadDto>>(rols);

            foreach (RolReadDto read in rolReadList)
            {
                read.Permitions = _mapper.Map<ICollection<PermitionReadDto>>(_permitionRepo.GetAllPermitionsByRol(read.Id));
            }

            return Ok(rolReadList);
        }


        [HttpGet("{id}", Name = "GetRoleById")]
        public ActionResult<RolReadDto> GetRoleById(int id)
        {
            var rolDb = _repo.GetRolById(id);
            if (rolDb == null) return NotFound();

            var rolRead = _mapper.Map<RolReadDto>(rolDb);
            rolRead.Permitions = _mapper.Map<ICollection<PermitionReadDto>>(_permitionRepo.GetAllPermitionsByRol(rolRead.Id));
            return Ok(rolRead);
        }

        [HttpPost]
        public ActionResult<RolReadDto> AddNewRol(RolAddDto dto)
        {
            var rol = _mapper.Map<Rol>(dto);
            rol.PermitionRols = new List<PermitionRol>();

            _repo.CreateRol(rol);
            _repo.SaveChanges();
            var rolRead = _mapper.Map<RolReadDto>(rol);
            rolRead.Permitions = _mapper.Map<ICollection<PermitionReadDto>>(_permitionRepo.GetAllPermitionsByRol(rolRead.Id));

            return CreatedAtRoute(
                nameof(GetRoleById),
                new { Id = rolRead.Id },
                rolRead

            );

        }
    }
}