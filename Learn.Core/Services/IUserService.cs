using Learn.Core.DTO;
using Learn.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.Services
{
    public interface IUserService:IService<User>
    {
        User GetByEmail(string email);
        Task<Token> Login(UserLoginDto userLoginDto);
    }
}
