using System.Net;
using WebAPIClone.Commom.Result;

namespace WebAPIClone.Common
{
    public class ApiResult<T>
    {
        public Code code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
