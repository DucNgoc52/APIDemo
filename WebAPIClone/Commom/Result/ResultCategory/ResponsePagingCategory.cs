using WebAPIClone.Model;

namespace WebAPIClone.Commom.Result.ResultCategory
{
    public class ResponsePagingCategory
    {
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int totalRecord { get; set; }
        public int totalPage { get; set; }
        public List<CategoryModel> content { get; set; }
    }
}
