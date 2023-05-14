using AccountService.Dtos;
using AccountService.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/v1/accounts/types")]
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountTypeRepo _repo;
        private readonly IMapper _mapper;

        public AccountTypeController(IMapper mapper, IAccountTypeRepo repo)
        {
            _repo = repo;
            _mapper = mapper;
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
        public ActionResult<TypeReadDto> GeTypeById(int id)
        {
            var type = _repo.GetTypeById(id);
            return type == null
                ? NotFound()
                : Ok(_mapper.Map<TypeReadDto>(type));
        }
    }

}