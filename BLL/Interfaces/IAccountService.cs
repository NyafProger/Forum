using BLL.DTO;
using BLL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        public Task<UserDTO> RegisterAsync(RegisterModel model);
        public Task<JwtSecurityToken> LogInAsync(LogInModel model);
    }
}
