using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovementService.AsyncDataService;
using MovementService.Dtos;
using MovementService.Models;
using MovementService.Repos;

namespace MovementService.Controllers
{
    [ApiController]
    [Route("api/v1/movements/accounts/{accountId}/movements")]
    public class MovementsController : ControllerBase
    {
        private readonly IMovementRepo _repo;
        private readonly IMapper _mapper;
        private readonly IMessageBus _messageBusClient;

        public MovementsController(
            IMapper mapper,
            IMovementRepo repo,
            IMessageBus messageBusClient
        )
        {
            _repo = repo;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovementReadDto>> GetAllMovementsByAccount(int accountId)
        {
            return Ok(
                _mapper.Map<IEnumerable<MovementReadDto>>(
                    _repo.GetAllMovements(accountId)
                )
            );
        }

        [HttpGet("{moveId}", Name = "GetSingleMovementById")]
        public ActionResult<MovementReadDto> GetSingleMovementById(
            int accountId,
            int moveId
        )
        {
            var Movement = _repo.GetMovementById(moveId, accountId);
            if (Movement == null) return NotFound();
            var movementRead = _mapper.Map<MovementReadDto>(Movement);
            movementRead.Type = _mapper.Map<TypeReadDto>(_repo.GetMovementType(Movement.TypeId));
            return Ok(movementRead);
        }


        [HttpPost]
        public ActionResult<MovementReadDto> AddMovementToAccount(
            int accountId,
            MovementAddDto dto
        )
        {
            var Movement = _mapper.Map<Movement>(dto);

            if (dto.TypeId != 1)
            {
                Movement.MovementAmount *= -1;
            }

            _repo.AddMovement(Movement, accountId);
            var ReadMovement = _mapper.Map<MovementReadDto>(Movement);
            SendAsyncMessage(ReadMovement, accountId);
            return CreatedAtRoute(
                nameof(GetSingleMovementById),
                new { accountId = accountId, moveId = ReadMovement.Id },
                ReadMovement
            );
        }


        private void SendAsyncMessage(MovementReadDto dto, int AccountId)
        {
            try
            {
                MovementPublishedDto accountPublished = _mapper.Map<MovementPublishedDto>(dto);
                accountPublished.AccountId = AccountId;
                accountPublished.Event = "Movement_Published";
                _messageBusClient.PublishNewMovement(accountPublished);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldn't send asynchronously message {ex.Message}");
            }
        }
    }
}