using System.Net;
using WebAPIClone.Commom.Result;

namespace WebAPIClone.Common
{
    public class ApiSuccesResult<T> : ApiResult<T>
    {
        public ApiSuccesResult(string message)
        {
            Message = message;
        }
        public ApiSuccesResult(T data, string message)
        {
            Message = message;
            Data = data;
        }
        public ApiSuccesResult(T data, string message, Code _code)
        {
            code = _code;
            Message = message;
            Data = data;
        }
    }
}
