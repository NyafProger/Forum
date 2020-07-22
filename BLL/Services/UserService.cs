using BLL.DTO;
using DAL.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using DAL.Entities;
using BLL.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;

namespace BLL.Services
{
    public class UserService: IUserService
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        IUnitOfWork _uow;

        public UserService(IUnitOfWork uow, 
                           IMapper mapper, 
                           UserManager<User> userManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
        }

        public UserDTO GetByName(string Name)
        {
            User user = _uow.Users.GetByName(Name);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            IEnumerable<User> users = _uow.Users.GetAll();
            var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);
            return usersDTO;
        }

        public UserDTO Get(int id)
        {
            User user = _uow.Users.Get(id);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        } 

        public UserDTO GetByEmail(string Email)
        {
            User user = _uow.Users.GetByEmail(Email);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> IdentifyUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            User user = await _userManager.GetUserAsync(claimsPrincipal);
            var userDTO = _mapper.Map<UserDTO>(user);
            userDTO.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return userDTO;
        }

        public async Task AddToRoleAsync(int id, string role)
        {
            var user = _uow.Users.Get(id);
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task RemoveFromRoleAsync(int id, string role)
        {
            var user = _uow.Users.Get(id);
            await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<IList<string>> GetUserRolesAsync(int id)
        {
            var user = _uow.Users.Get(id);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IList<UserDTO>> GetUsersByRoleAsync(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);
            var usersDto = _mapper.Map<IList<UserDTO>>(users);
            return usersDto;
        }
    }
}
