
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService: IAccountService
    {
        private UserManager<User> _userManager;
        private IUnitOfWork _uow;
        private IMapper _mapper;

        public AccountService(UserManager<User> userManager,
                              IUnitOfWork uow, 
                              IMapper mapper)
        {
            _userManager = userManager;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<UserDTO> RegisterAsync(RegisterModel model)
        {
            User user = new User()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Email = model.Email,
                PictureUrl = "",
            };
            var regResult = await _userManager.CreateAsync(user, model.Password);
            var roleResult = await _userManager.AddToRoleAsync(user, Roles.User);
            if (regResult.Succeeded && roleResult.Succeeded)
            {
                return  _mapper.Map<UserDTO>(_uow.Users.GetByName(user.UserName));
            }
            return null;
        }

        public async Task<JwtSecurityToken> LogInAsync(LogInModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                };
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConfig.Key));

                var token = new JwtSecurityToken(
                    issuer: TokenConfig.Issuer,
                    audience: TokenConfig.Audience,
                    expires: TokenConfig.LifeTime,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );
                return token;
            }
            return null;
        }
    }
}

