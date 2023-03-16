using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIClone.Commom.MSG;
using WebAPIClone.Common;
using WebAPIClone.Data;
using WebAPIClone.Model;

namespace WebAPIClone.Repository.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ApiResult<string>> SignInAsync(SignInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>(MsgError.LOGIN_FAILED);
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha512Signature);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidAudience"],
                audience: _configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddMinutes(15),
                claims: claims,
                signingCredentials: signingCredentials
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwtSecurityToken);
            return new ApiSuccesResult<string>(token, MsgSuccess.LOGIN_SUCCESS, Commom.Result.Code.OK);
        }

        public async Task<ApiResult<bool>> SignUpAsync(SignUpModel model)
        {
            if(await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return new ApiErrorResult<bool>(MsgError.SIGNIN_FAILED);
            }
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            var rs = await _userManager.CreateAsync(user, model.Password);
            if (!rs.Succeeded)
            {
                return new ApiErrorResult<bool>(MsgError.PASS_NOT_VALID);
            }
            return new ApiSuccesResult<bool>(true, MsgSuccess.SIGNIN_SUCCESS);
        }
    }
}
