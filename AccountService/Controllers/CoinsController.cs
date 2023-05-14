using AccountService.Dtos;
using AccountService.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/v1/accounts/coins")]
    public class CoinsController : ControllerBase
    {
        private readonly ICoinTypeRepo _repo;
        private readonly IMapper _mapper;

        public CoinsController(ICoinTypeRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CoinReadDto>> GetAllCoins()
        {
            return Ok(
                _mapper.Map<IEnumerable<CoinReadDto>>(
                    _repo.GetAllCoins()
                )
            );
        }

        [HttpGet("{id}")]
        public ActionResult<CoinReadDto> GetCoinById(int id)
        {
            var coin = _repo.GetCoinById(id);
            return coin == null
                ? NotFound()
                : Ok(_mapper.Map<CoinReadDto>(coin));
        }

    }
}