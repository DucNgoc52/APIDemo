using System.Net;
using WebAPIClone.Commom.Result;

namespace WebAPIClone.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public ApiErrorResult()
        {

        }
        public ApiErrorResult(string message)
        {
            Message = message;
        }
        public ApiErrorResult(string message, Code _code)
        {
            code = _code;
            Message = message;
        }
    }
}
