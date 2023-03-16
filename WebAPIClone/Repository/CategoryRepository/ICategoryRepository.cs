using WebAPIClone.Commom.Result.ResultCategory;
using WebAPIClone.Common;
using WebAPIClone.Data;
using WebAPIClone.Model;

namespace WebAPIClone.Repository.CategoryRepository
{
    public interface ICategoryRepository
    {
        public ApiResultListCategory GetAllCategoryAsync(string search, int page, int pageSize);
        public Task<ApiResult<CategoryModel>> GetCateById(int id);
        public Task<ApiResult<CategoryModel>> AddCategoryAsync(CategoryModel model);
        public Task<ApiResult<bool>> UpdateCategoryAsync(int id, CategoryModel model);
        public Task<ApiResult<bool>> DeleteCategoryAsync(int id);
    }
}
