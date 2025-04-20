using Learn.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.Services
{
    public interface ITokenHandler
    {
        Token CreateToken(User user,List<Role> roles);
        string CreateRefreshToken();
        IEnumerable<Claim> SetClaims(User user, List<Role> roles);
    }
}
