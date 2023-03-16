using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPIClone.Commom.MSG;
using WebAPIClone.Commom.Result;
using WebAPIClone.Commom.Result.ResultCategory;
using WebAPIClone.Common;
using WebAPIClone.Data;
using WebAPIClone.Model;

namespace WebAPIClone.Repository.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ApiResultListCategory GetAllCategoryAsync(string search, int page, int pageSize)
        {
            var cates = _context.Categories.AsQueryable();
            //search
            if (!string.IsNullOrEmpty(search))
            {
                cates = _context.Categories.Where(b => b.Name.Contains(search));
            }

            cates = cates.OrderBy(b => b.Name);

            //paging
            int totalCount = cates.Count();
            cates = cates.Skip((page - 1) * pageSize).Take(pageSize);

            var result = cates.Select(b => new CategoryModel
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description
            });
            var tempCates = result.ToList();
            return new ApiResultListCategory()
            {
                code = Code.OK,
                message = MsgSuccess.GET_ITEM_SUCCESS,
                data = new ResponsePagingCategory()
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalRecord = totalCount,
                    totalPage = (int)Math.Ceiling(totalCount / (double)pageSize),
                    content = tempCates
                }
            };
        }

        public async Task<ApiResult<CategoryModel>> GetCateById(int id)
        {
            var cate = await _context.Categories.FindAsync(id);
            if (cate == null)
            {
                return new ApiErrorResult<CategoryModel>(MsgError.GET_ITEM_byID_FAILED, Code.OK);
            }
            var catemodel = _mapper.Map<CategoryModel>(cate);
            return new ApiSuccesResult<CategoryModel>(catemodel, MsgSuccess.GET_ITEM_SUCCESS, Code.OK);
        }

        public async Task<ApiResult<CategoryModel>> AddCategoryAsync(CategoryModel model)
        {
            var cate = _mapper.Map<Category>(model);
            _context.Add(cate);
            await _context.SaveChangesAsync();
            return new ApiSuccesResult<CategoryModel>(model, MsgSuccess.ITEM_CREATE_SUCCESS, Code.OK);
        }

        public async Task<ApiResult<bool>> UpdateCategoryAsync(int id, CategoryModel model)
        {
            if(id == model.Id)
            {
                var cate = _mapper.Map<Category>(model);
                _context.Categories.Update(cate);
                await _context.SaveChangesAsync();
                return new ApiSuccesResult<bool>(true,MsgSuccess.ITEM_UPDATE_SUCCESS, Code.OK);
            }
            return new ApiErrorResult<bool>(MsgError.ITEM_UPDATE_FAILED, Code.OK);
        }

        public async Task<ApiResult<bool>> DeleteCategoryAsync(int id)
        {
            var cate = await _context.Categories.FindAsync(id);
            if (cate != null)
            {
                _context.Categories.Remove(cate);
                await _context.SaveChangesAsync();
                return new ApiSuccesResult<bool>(true, MsgSuccess.ITEM_DELETE_SUCCESS, Code.OK);
            }
            else
            {
                return new ApiErrorResult<bool>(MsgError.GET_ITEM_byID_FAILED, Code.OK);
            }

        }
    }
}
