using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
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
        public Task AddToRoleAsync(UserDTO userDto, string role);
        public Task RemoveFromRoleAsync(UserDTO userDto, string role);
        public Task<IList<string>> GetUserRolesAsync(UserDTO userDto);
        public Task<IList<UserDTO>> GetUsersByRoleAsync(string role);
    }
}
