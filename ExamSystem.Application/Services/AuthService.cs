using AutoMapper;
using ExamSystem.Application.Dtos.Authentication;
using ExamSystem.Application.Helper;
using ExamSystem.Application.Services.Contract;
using ExamSystem.Domain.Entities.Identity;
using ExamSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace ExamSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(
            AppDbContext db,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IMapper mapper
            )
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AuthResponseModel> LoginAsync(LoginDto model)
        {
            var authModel = new AuthResponseModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;

            return authModel;

        }
        public async Task<AuthResponseModel> RegisterAsync(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthResponseModel { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthResponseModel { Message = "Username is already registered!" };

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = new StringBuilder();
                foreach (var error in result.Errors)
                    errors.Append($"{error.Description},");

                return new AuthResponseModel { Message = errors.ToString() };
            }

            if (model.UserTypeId == 1)
            {
                await _userManager.AddToRoleAsync(user, "JOBSEEKER");
            }
            else if (model.UserTypeId == 2)
            {
                await _userManager.AddToRoleAsync(user, "COMPANY");
            }



            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthResponseModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }
        public async Task<AuthResponseModel> RegisterAdminAsync(RegisterAdminDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthResponseModel { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthResponseModel { Message = "Username is already registered!" };

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = new StringBuilder();
                foreach (var error in result.Errors)
                    errors.Append($"{error.Description},");

                return new AuthResponseModel { Message = errors.ToString() };
            }

            await _userManager.AddToRoleAsync(user, "ADMIN");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthResponseModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var roleClaims = userRoles.Select(r => new Claim("roles", r)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(int.Parse(_configuration["JWT:DurationInDays"]!)),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }



        public async Task DeleteUserAsync(string id)
        {
            var result = await _db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result != null) { _db.Users.Remove(result); }
        }

        public async Task<IEnumerable<ResponseUserDetailsDto>> GetAllUsersAsync()
        {
            var users = await _db.Users.ToListAsync();

            var usersResponseDto = _mapper.Map<IEnumerable<ResponseUserDetailsDto>>(users);

            return usersResponseDto;
        }

        public async Task<ResponseUserDetailsDto?> GetUserByIdAsync(string id)
        {
            var user = await _db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            var userResponseDto = _mapper.Map<ResponseUserDetailsDto>(user);

            return userResponseDto;
        }

        public async Task<ResponseUserDetailsDto> UpdateUserAsync(UpdateUserDetailsDto Dto)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(x => x.Id == Dto.Id);

            if (existingUser is null) throw new KeyNotFoundException("User Id Not Found");

            UpdateObjectHelper.UpdateObject(existingUser, Dto);

            await _db.SaveChangesAsync();

            var responseUserDto = _mapper.Map<ResponseUserDetailsDto>(existingUser);

            return responseUserDto;
        }

        public Task<bool> ChangePassword(string userId, string password)
        {
            throw new NotImplementedException();
        }
    }
}
