using AccountService.AsyncDataServices;
using AccountService.Dtos;
using AccountService.Models;
using AccountService.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/v1/accounts/users/{userId}/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _repo;
        private readonly IMapper _mapper;
        private readonly IMessageBus _messageBusClient;

        public AccountController(
            IMapper mapper,
            IAccountRepo repo,
            IMessageBus messageBusClient)
        {
            _repo = repo;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AccountReadDto>> GetAllAccounts(int userId)
        {
            var accounts = _repo.GetAccountsForUser(userId);
            var response = new List<AccountReadDto>();
            foreach (Account account in accounts)
            {
                var coin = _mapper.Map<CoinReadDto>(_repo.GetAccountCoin(account.CoinId));
                var type = _mapper.Map<TypeReadDto>(_repo.GetAccountType(account.TypeId));
                var accountRead = _mapper.Map<AccountReadDto>(account);
                accountRead.Coin = coin;
                accountRead.Type = type;
                response.Add(accountRead);

            }
            return Ok(
                _mapper.Map<IEnumerable<AccountReadDto>>(
                    response
                )
            );
        }

        [HttpGet("{accountId}", Name = "GetSingleAccountById")]
        public ActionResult<AccountReadDto> GetSingleAccountById(int accountId)
        {
            var account = _repo.GetAccountById(accountId);
            if (account == null) return NotFound();

            var coin = _mapper.Map<CoinReadDto>(_repo.GetAccountCoin(account.CoinId));
            var type = _mapper.Map<TypeReadDto>(_repo.GetAccountType(account.TypeId));
            var accountRead = _mapper.Map<AccountReadDto>(account);
            accountRead.Coin = coin;
            accountRead.Type = type;
            return Ok(accountRead);
        }

        [HttpPost]
        public ActionResult<AccountReadDto> AddNewAccount(int userId, AccountAddDto dto)
        {
            var account = _mapper.Map<Account>(dto);
            account.ActualAmount = dto.InitialAmount;
            account.CoinId = dto.CoinId;
            account.TypeId = dto.TypeId;
            account.UserId = userId;

            _repo.CreateAccount(account);

            var coin = _mapper.Map<CoinReadDto>(_repo.GetAccountCoin(account.CoinId));
            var type = _mapper.Map<TypeReadDto>(_repo.GetAccountType(account.TypeId));
            var accountRead = _mapper.Map<AccountReadDto>(account);
            accountRead.Coin = coin;
            accountRead.Type = type;
            SendAsyncMessage(accountRead);
            return CreatedAtRoute(
                nameof(GetSingleAccountById),
                new { userId = account.UserId, accountId = account.Id },
                accountRead
            );
        }

        private void SendAsyncMessage(AccountReadDto dto)
        {
            try
            {
                AccountPublishedDto accountPublished = _mapper.Map<AccountPublishedDto>(dto);
                accountPublished.Event = "Account_Published";
                _messageBusClient.PublishNewAccount(accountPublished);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldn't send asynchronously message {ex.Message}");
            }
        }
    }
}