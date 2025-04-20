using Learn.Core.DTO;
using Learn.Core.Models;
using Learn.Core.Repositories;
using Learn.Core.Services;
using Learn.Core.UnitOfWorks;
using Learn.Service.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;




namespace Learn.Service.Services
{
    public class UserService : Service<User>, IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;
        public UserService(IGenericRepository<User> repository, IUnitOfWorks unitOfWorks, IUserRepository userRepository, ITokenHandler tokenHandler) : base(repository, unitOfWorks)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        public User GetByEmail(string email)
        {
            User user= _userRepository.Where(x=>x.Email==email)
                .Include(x=>x.Group).
                ThenInclude(g=>g.GroupInRoles)
                .ThenInclude(x=>x.Role)
                .FirstOrDefault();
            return user ?? user;
        }

        public async Task<Token> Login(UserLoginDto userLoginDto)
        {
           Token token = new Token();
            var user =GetByEmail(userLoginDto.Email);
            if(user == null)
            {
                return null;
            }
            var result=false;

            result = HashingHelper.VerifyPasswordHash(userLoginDto.Password,user.PassWordHash, user.PasswordSalt);
            
            if (result)
            {
                var roles= user.Group.GroupInRoles.Select(x=>x.Role).ToList();
                token = _tokenHandler.CreateToken(user, roles);
                return token;
            }
            return null ;
        }
    }
}
