using AuthService.AsyncDataServices;
using AuthService.Dtos;
using AuthService.Models;
using AuthService.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/v1/auth/users")]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private IUserRepo _repo;
        private IPermitionRepo _permitionRepo;
        private IMessageBus _messageBusClient;

        public UserController(
            IMapper mapper,
            IUserRepo repo,
            IPermitionRepo permitionRepo,
            IMessageBus messageBusClient
        )
        {
            _mapper = mapper;
            _repo = repo;
            _permitionRepo = permitionRepo;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = _repo.GetAllUsers();
            List<UserReadDto> response = new List<UserReadDto>();

            foreach (User singleUser in users)
            {
                var rolRead = _mapper.Map<RolReadDto>(_repo.GetUserRol(singleUser.RoleId));
                var userRead = _mapper.Map<UserReadDto>(singleUser);

                rolRead.Permitions = _mapper.Map<ICollection<PermitionReadDto>>(
                    _permitionRepo.GetAllPermitionsByRol(rolRead.Id)
                );

                userRead.UserRole = rolRead;
                response.Add(userRead);
                // singleUser.UserRole = rolRead;
            }

            return Ok(response);
        }


        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<UserReadDto> GetUserById(int id)
        {
            User user = _repo.GetUserById(id);
            if (user == null) return NotFound();
            var rolRead = _mapper.Map<RolReadDto>(_repo.GetUserRol(user.RoleId));
            var userRead = _mapper.Map<UserReadDto>(user);

            rolRead.Permitions = _mapper.Map<ICollection<PermitionReadDto>>(
                _permitionRepo.GetAllPermitionsByRol(rolRead.Id)
            );

            userRead.UserRole = rolRead;
            return Ok(userRead);
        }


        [HttpPost]
        public ActionResult<UserReadDto> AddUser(AddUserDto dto)
        {

            if (_repo.GetUserByIdentification(dto.Identification) != null)
                return BadRequest("Identificacion de usuario ya existe!");

            User user = _mapper.Map<User>(dto);
            user.RoleId = dto.RoleId;
            _repo.CreateUser(user);
            _repo.SaveChanges();

            var rolRead = _mapper.Map<RolReadDto>(_repo.GetUserRol(user.RoleId));
            var userRead = _mapper.Map<UserReadDto>(user);

            rolRead.Permitions = _mapper.Map<ICollection<PermitionReadDto>>(
                _permitionRepo.GetAllPermitionsByRol(rolRead.Id)
            );

            userRead.UserRole = rolRead;

            // Sending user created method
            SendAsyncMessage(userRead);

            return CreatedAtRoute(
                nameof(GetUserById),
                new { Id = userRead.Id },
                userRead
            );
        }

        [HttpPut("{id}")]
        public ActionResult<UserReadDto> UpdateUser(int id, AddUserDto dto)
        {
            // Getting user tu update
            User user = _repo.GetUserById(id);
            if (user == null) return NotFound();
            // Updating info
            User userNewData = _mapper.Map<User>(dto);
            userNewData.Id = user.Id;
            userNewData.Identification = user.Identification;
            userNewData.RoleId = dto.RoleId;
            _repo.UpdateUser(userNewData);

            var rolRead = _mapper.Map<RolReadDto>(_repo.GetUserRol(userNewData.RoleId));
            var userRead = _mapper.Map<UserReadDto>(userNewData);

            rolRead.Permitions = _mapper.Map<ICollection<PermitionReadDto>>(
                _permitionRepo.GetAllPermitionsByRol(rolRead.Id)
            );

            userRead.UserRole = rolRead;
            return Ok(userRead);
        }


        private void SendAsyncMessage(UserReadDto dto)
        {
            try
            {
                UserPublishedDto userPublished = _mapper.Map<UserPublishedDto>(dto);
                userPublished.Event = "User_Published";
                _messageBusClient.PublishNewUser(userPublished);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldn't send asynchronously message {ex.Message}");
            }
        }
    }
}