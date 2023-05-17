using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovementService.Dtos;
using MovementService.Models;
using MovementService.Repos;

namespace MovementService.Controllers
{
    [ApiController]
    [Route("api/v1/movements/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepo _repo;

        public AccountController(
            IMapper mapper,
            IAccountRepo repo
        )
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AccountReadDto>> GetAllAccounts()
        {
            return Ok(
                _mapper.Map<IEnumerable<AccountReadDto>>(
                    _repo.GetAllAccounts()
                )
            );
        }

        [HttpGet("{id}")]
        public ActionResult<AccountReadDto> GetSingleAccountById(int id)
        {
            Account account = _repo.GetAccountById(id);
            return (account == null)
             ? NotFound()
             : Ok(_mapper.Map<AccountReadDto>(account));
        }

    }

}