using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovementService.Dtos;
using MovementService.Repos;

namespace MovementService.Controllers
{
    [ApiController]
    [Route("api/v1/movements/types")]
    public class TypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITypeRepo _repo;

        public TypeController(IMapper mapper, ITypeRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TypeReadDto>> GetAllTypes()
        {
            return Ok(
                _mapper.Map<IEnumerable<TypeReadDto>>(
                    _repo.GetAllTypes()
                )
            );
        }

        [HttpGet("{id}")]
        public ActionResult<TypeReadDto> GetSingleType(int id)
        {
            Console.WriteLine($"-> Looking for Type with id: {id}");
            var Type = _repo.GetMovementTypeById(id);
            return Type == null
                ? NotFound()
                : Ok(_mapper.Map<TypeReadDto>(Type));
        }
    }
}