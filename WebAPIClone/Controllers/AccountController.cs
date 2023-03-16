using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIClone.Model;
using WebAPIClone.Repository.AccountRepository;

namespace WebAPIClone.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repoAccount;

        public AccountController(IAccountRepository repo)
        {
            _repoAccount = repo;
        }
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repoAccount.SignUpAsync(model);
            return Ok(result);
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repoAccount.SignInAsync(model);
            return Ok(result);
        }    
    }
}
