namespace WebAPIClone.Commom.Result.ResultCategory
{
    public class ApiResultListCategory
    {
        public Code code { get; set; }
        public string message { get; set; }
        public ResponsePagingCategory data { get; set; }
    }
}
