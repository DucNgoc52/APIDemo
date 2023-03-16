using WebAPIClone.Model;

namespace WebAPIClone.Commom.Result
{
    public class ApiResultListBook
    {
        public Code code { get; set; }
        public string message { get; set; }
        public ResponsePagingBook data { get; set; }
    }

}
