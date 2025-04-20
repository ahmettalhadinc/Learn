using AutoMapper;
using Learn.API.Filters;
using Learn.Core.DTO.UpdateDTOs;
using Learn.Core.DTO;
using Learn.Core.Models;
using Learn.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Learn.Service.Hashing;

namespace Learn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomBaseController

    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = _userService.GetAll();
            var dtos = _mapper.Map<List<UserDto>>(users).ToList();
            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, userDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            int userId = 1;
            var user = await _userService.GetByIdAsync(id);
            user.UpdatedBy = userId;
            _userService.ChangeStatus(user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserDto userDto)
        {
            int userId = 1;


            var processedEntity = _mapper.Map<User>(userDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userDto.Password, out passwordHash, out passwordSalt);

            processedEntity.PassWordHash = passwordHash;
            processedEntity.PasswordSalt = passwordSalt;

            var user = await _userService.AddAsync(processedEntity);
            var userResponseDto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(201, userDto));

        }
        [HttpPut]

        public async Task<IActionResult> Update(UserUpdateDto userDto)
        {
            var userId = 1;

            var currentUser = await _userService.GetByIdAsync(userDto.Id);
            currentUser.UpdatedBy = userId;
            currentUser.name = userDto.name;
            _userService.Update(currentUser);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            Token token = await _userService.Login(userLoginDto);
            if (token == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(401, "Bilgiler Uyuşmuyor"));

            }
            return CreateActionResult(CustomResponseDto<Token>.Success(200, token));
        }
    }

}
