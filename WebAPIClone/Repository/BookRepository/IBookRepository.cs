using Microsoft.AspNetCore.JsonPatch;
using WebAPIClone.Commom.Result;
using WebAPIClone.Common;
using WebAPIClone.Data;
using WebAPIClone.Model;
using WebAPIClone.Model.Book;

namespace WebAPIClone.Repository
{
    public interface IBookRepository
    {
        public ApiResultListBook GetAllBookAsync(string search, string sort, int page, int pageSize);
        public Task<ApiResult<BookModel>> GetBookByIdAsync(int id);
        public Task<ApiResult<Book>> AddBookAsync(BookCreateModel model);
        public Task<ApiResult<List<Book>>> AddBookAsync(List<BookCreateModel> model);
        public Task<ApiResult<Book>> UpdateBookAsync(int id, BookModel model);
        public Task<ApiResult<List<Book>>> UpdateBookAsync(List<BookModel> model);
        public Task<ApiResult<Book>> PatchUpdateBookAsync(int id, BookPatchModel model);
        public Task<ApiResult<Book>> DeleteBookAsync(int id);
        public Task<ApiResult<List<Book>>> DeleteBookAsync(int[] ids);
    }
}
