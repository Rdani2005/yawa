using AccountService.Dtos;
using AccountService.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/v1/accounts/users")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _repo;

        public UserController(IMapper mapper, IUserRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = _repo.GetAllUsers();


            return Ok(
                _mapper.Map<IEnumerable<UserReadDto>>(users)
            );
        }

        [HttpGet("{id}")]
        public ActionResult<UserReadDto> GetSingleUserById(int id)
        {
            var user = _repo.GetUserById(id);
            return user == null
                ? NotFound()
                : _mapper.Map<UserReadDto>(user);
        }
    }
}