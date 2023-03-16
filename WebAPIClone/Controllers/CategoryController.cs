using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIClone.Commom.Result.ResultCategory;
using WebAPIClone.Common;
using WebAPIClone.Model;
using WebAPIClone.Repository.CategoryRepository;

namespace WebAPIClone.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repoCate;

        public CategoryController(ICategoryRepository repoCate)
        {
            _repoCate = repoCate;
        }
        [HttpGet]
        public  ApiResultListCategory GetAllCateAsync(string search, int page = 1, int pageSize = 10)
        {
            var result =  _repoCate.GetAllCategoryAsync(search, page, pageSize);
            return result;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ApiResult<CategoryModel>>> GetCatebyIdAsync(int id)
        {
            var result = await _repoCate.GetCateById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<CategoryModel>>> AddCateAsync(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repoCate.AddCategoryAsync(model);
            return result;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ApiResult<bool>>> UpdateCateAsync(int id, CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repoCate.UpdateCategoryAsync(id, model);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ApiResult<bool>>> DeleteCateSync(int id)
        {
            var result = await _repoCate.DeleteCategoryAsync(id);
            return Ok(result);
        }
    }
}
