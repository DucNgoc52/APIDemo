using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPIClone.Commom.MSG;
using WebAPIClone.Commom.Result;
using WebAPIClone.Common;
using WebAPIClone.Data;
using WebAPIClone.Model;
using WebAPIClone.Model.Book;

namespace WebAPIClone.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;
        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ApiResultListBook GetAllBookAsync(string search, string sort, int page, int pageSize)
        {
            var books =  _context.Books.AsQueryable();
            //search
            if (!string.IsNullOrEmpty(search))
            {
                books =  _context.Books.Where(b => b.Title.Contains(search));
            }

            books = books.OrderBy(b => b.Title);

            //sort
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "-title":
                        books = books.OrderByDescending(b => b.Title);
                        break;
                    case "+price":
                        books = books.OrderBy(b => b.Price);
                        break;
                    case "-price":
                        books = books.OrderByDescending(b => b.Price);
                        break;
                }
            }

            //paging
            int totalCount = books.Count();
            books = books.Skip((page - 1) * pageSize).Take(pageSize);

            var result = books.Select(b => new BookModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Price = b.Price,
                Quantity = b.Quantity,
                CategoryId = b.CategoryId
            });
            var tempBooks = result.ToList();
            return new ApiResultListBook()
            {
                code = Code.OK,
                message = MsgSuccess.GET_ITEM_SUCCESS,
                data = new ResponsePagingBook()
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalRecords = totalCount,
                    totalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                    content = tempBooks
                }
            };
        }
        public async Task<ApiResult<BookModel>> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return new ApiErrorResult<BookModel>(MsgError.GET_ITEM_byID_FAILED, Code.OK);
            }
            var mapBook = _mapper.Map<BookModel>(book);
            return new ApiSuccesResult<BookModel>(mapBook, MsgSuccess.GET_ITEM_SUCCESS, Code.OK);
        }
        public async Task<ApiResult<Book>> AddBookAsync(BookCreateModel model)
        {
            var res = await AddBookAsync(new List<BookCreateModel> {model});
            var result = new ApiResult<Book>();
            result.Data = res.Data.FirstOrDefault();
            return new ApiSuccesResult<Book>(result.Data, MsgSuccess.ITEM_CREATE_SUCCESS, Code.OK);
        }
        public async Task<ApiResult<List<Book>>> AddBookAsync(List<BookCreateModel> model)
        {
            List<Book> books = new List<Book>();
            foreach(var book in model)
            {
                var newbook = _mapper.Map<Book>(book);
                newbook.Id = 0;
                books.Add(newbook);
            }
            _context.Books.AddRange(books);
            await _context.SaveChangesAsync();
            return new ApiSuccesResult<List<Book>>(books, MsgSuccess.ITEM_CREATE_SUCCESS, Code.OK);
        }
        public async Task<ApiResult<Book>> UpdateBookAsync(int id, BookModel model)
        {
            var res = await UpdateBookAsync(new List<BookModel> { model });
            var result = new ApiResult<Book>();
            result.Data = res.Data.FirstOrDefault();
            return new ApiSuccesResult<Book>(result.Data, MsgSuccess.ITEM_UPDATE_SUCCESS, Code.OK);
        }
        public async Task<ApiResult<List<Book>>> UpdateBookAsync(List<BookModel> model)
        {
            List<Book> books = new List<Book>();
            foreach (var bookud in model)
            {
                var book = _mapper.Map<Book>(bookud);
                books.Add(book);
            }
            _context.Books.UpdateRange(books);
            await _context.SaveChangesAsync();
            return new ApiSuccesResult<List<Book>>(books, MsgSuccess.ITEM_UPDATE_SUCCESS, Code.OK);
        }
        public async Task<ApiResult<Book>> PatchUpdateBookAsync(int id, BookPatchModel model)
        {
            var book = await _context.Books.FindAsync(id);
            if(book == null)
            {
                return new ApiErrorResult<Book>(MsgError.ID_DOESNT_EXITS, Code.OK);
            }
            if(model.Title != null)
            {
                book.Title = model.Title;
            }
            if(model.Description != null)
            {
                book.Description = model.Description;
            }
            if(model.Price != null)
            {
                book.Price = (double)model.Price;
            }
            if (model.Quantity != null)
            {
                book.Quantity = (int)model.Quantity;
            }
            if(model.CategoryId != null)
            {
                book.CategoryId = (int)model.CategoryId;
            }
            await _context.SaveChangesAsync();
            var bookedit = await _context.Books.FindAsync(id);
            return new ApiSuccesResult<Book>(bookedit, MsgSuccess.ITEM_UPDATE_SUCCESS, Code.OK);
        }
        public async Task<ApiResult<Book>> DeleteBookAsync(int id)
        {
            var res = await DeleteBookAsync(new int[] { id });
            var result = new ApiResult<Book>();
            result.Data = res.Data.FirstOrDefault();
            return new ApiSuccesResult<Book>(result.Data, MsgSuccess.ITEM_DELETE_SUCCESS, Code.OK);
        }
        public async Task<ApiResult<List<Book>>> DeleteBookAsync(int[] ids)
        {
            List<Book> books = new List<Book>();
            foreach(var id in ids)
            {
                var book = await _context.Books.FindAsync(id);
                if(book == null)
                {
                    return new ApiErrorResult<List<Book>>(MsgError.GET_ITEM_byID_FAILED, Code.OK);
                }
                books.Add(book);
            }
            _context.Books.RemoveRange(books);
            await _context.SaveChangesAsync();
            return new ApiSuccesResult<List<Book>>(books, MsgSuccess.ITEM_DELETE_SUCCESS, Code.OK);
        }
    }
}
