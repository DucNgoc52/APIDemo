using WebAPIClone.Model;

namespace WebAPIClone.Commom.Result
{
    public class ResponsePagingBook
    {
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; }
        public int totalRecords { get; set; }
        public List<BookModel> content { get; set; }
    }
}
