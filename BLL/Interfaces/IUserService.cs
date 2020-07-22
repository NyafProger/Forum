using BLL.DTO;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        public UserDTO GetByName(string Name);
        public IEnumerable<UserDTO> GetAll();
        public UserDTO Get(int id);
        public UserDTO GetByEmail(string Email);
        public Task<UserDTO> IdentifyUserAsync(ClaimsPrincipal claimsPrincipal);
        public Task AddToRoleAsync(int id, string role);
        public Task RemoveFromRoleAsync(int id, string role);
        public Task<IList<string>> GetUserRolesAsync(int id);
        public Task<IList<UserDTO>> GetUsersByRoleAsync(string role);
    }
}
