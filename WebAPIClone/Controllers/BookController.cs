using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPIClone.Commom.Result;
using WebAPIClone.Common;
using WebAPIClone.Data;
using WebAPIClone.Model;
using WebAPIClone.Model.Book;
using WebAPIClone.Repository;

namespace WebAPIClone.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        public BookController(IBookRepository repo)
        {
            _bookRepo = repo;
        }
        [HttpGet]
        public ApiResultListBook GetAllBookAsync(string search, string sort, int page = 1, int pageSize = 10)
        {
            var results = _bookRepo.GetAllBookAsync(search, sort, page, pageSize);
            return results;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<BookModel>>> GetBookIdAsync(int id)
        {
            var book = await _bookRepo.GetBookByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<BookCreateModel>>> AddBookAsync(BookCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookRepo.AddBookAsync(model);
            return Ok(result);
        }
        [HttpPost]
        [Route("many")]
        public async Task<ActionResult<ApiResult<List<BookCreateModel>>>> AddListBook(List<BookCreateModel> model)
        {
            var result = await _bookRepo.AddBookAsync(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Book>>> UpdateBookAsync(int id, BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookRepo.UpdateBookAsync(id, model);
            return Ok(result);
        }
        [HttpPut]
        [Route("many")]
        public async Task<ActionResult<ApiResult<List<BookModel>>>> UpdateBookAsync(List<BookModel> model)
        {
            var result = await _bookRepo.UpdateBookAsync(model);
            return Ok(result);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResult<Book>>> PatchUpdateBookAsync(int id, BookPatchModel model)
        {
            var result = await _bookRepo.PatchUpdateBookAsync(id, model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Book>>> DeleteBookAsync(int id)
        {
            var result = await _bookRepo.DeleteBookAsync(id);
            return Ok(result);
        }
        [HttpDelete]
        [Route("many")]
        public async Task<ActionResult<ApiResult<List<Book>>>> DeleteListBookAsync(int[] id)
        {
            var result = await _bookRepo.DeleteBookAsync(id);
            return Ok(result);
        }
    }
}
