using Microsoft.AspNetCore.Identity;
using WebAPIClone.Common;
using WebAPIClone.Model;

namespace WebAPIClone.Repository.AccountRepository
{
    public interface IAccountRepository
    {
        public Task<ApiResult<bool>> SignUpAsync(SignUpModel model);
        public Task<ApiResult<string>> SignInAsync(SignInModel model);
    }
}
