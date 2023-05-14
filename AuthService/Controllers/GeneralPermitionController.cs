using AuthService.AsyncDataServices;
using AuthService.Dtos;
using AuthService.Models;
using AuthService.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/v1/auth/permitions")]
    public class GeneralPermitionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPermitionRepo _permitionRepo;
        private readonly IPermitionRolRepo _rolPermitionRepo;
        private readonly IRolRepo _rolRepo;
        private readonly IMessageBus _messageBus;

        public GeneralPermitionController(
            IMapper mapper,
            IPermitionRepo permitionRepo,
            IPermitionRolRepo rolPermitionRepo,
            IRolRepo rolRepo,
            IMessageBus messageBus
            )
        {
            _mapper = mapper;
            _permitionRepo = permitionRepo;
            _rolPermitionRepo = rolPermitionRepo;
            _rolRepo = rolRepo;
            _messageBus = messageBus;
        }


        [HttpGet]
        public ActionResult<IEnumerable<PermitionReadDto>> GetAllPermitions()
        {
            return Ok(
                _mapper.Map<IEnumerable<PermitionReadDto>>(_permitionRepo.GetAllPermitions())
            );
        }


        [HttpGet("{id}", Name = "GetGeneralPermitionById")]
        public ActionResult<PermitionReadDto> GetGeneralPermitionById(int id)
        {
            var permition = _permitionRepo.GetPermitionById(id);
            return permition == null
                ? NotFound()
                : Ok(_mapper.Map<PermitionReadDto>(permition));
        }

        [HttpPost]
        public ActionResult AddPermition(PermitionAddDto dto)
        {
            Permition permition = _mapper.Map<Permition>(dto);
            permition.PermitionRols = new List<PermitionRol>();
            _permitionRepo.CreatePermition(permition);
            _permitionRepo.SaveChanges();
            PermitionReadDto permitionReadDto = _mapper.Map<PermitionReadDto>(permition);
            // Sending message to Rabbit MQ
            SendAsyncMessage(permitionReadDto);
            return CreatedAtRoute(
                nameof(GetGeneralPermitionById),
                new { Id = permition.Id },
                permitionReadDto
            );
        }

        [HttpPost("relation")]
        public ActionResult AddPermitioRealtion(AddRelationPermitionRol relation)
        {
            var rol = _rolRepo.GetRolById(relation.RolId);
            var permition = _permitionRepo.GetPermitionById(relation.PermitionId);
            Console.WriteLine($"--> Rol a usar: {rol.Name}");
            Console.WriteLine($"--> Permiso a usar: {permition.Name}");
            if (rol == null || permition == null)
                return BadRequest("No se encuentra el rol o el permiso");

            _rolPermitionRepo.CreateRelationPermitionRol(
                rol, permition
            );
            _rolRepo.SaveChanges();

            return Ok();
        }


        private void SendAsyncMessage(PermitionReadDto dto)
        {
            try
            {
                PermitionPublishedDto permitionPublished = _mapper.Map<PermitionPublishedDto>(dto);
                permitionPublished.Event = "Permition_Published";
                _messageBus.PublishNewPermition(permitionPublished);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldn't send asynchronously message {ex.Message}");
            }
        }
    }
}