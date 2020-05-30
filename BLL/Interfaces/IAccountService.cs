using BLL.DTO;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        public Task<UserDTO> RegisterAsync(RegisterModel model);
        public Task<JwtSecurityToken> LogInAsync(LogInModel model);
    }
}
