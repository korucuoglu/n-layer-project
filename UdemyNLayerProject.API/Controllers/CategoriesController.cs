using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyNLayerProject.API.Filters;
using UdemyNLayerProject.API.Service;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Data.DTOs.Categories;

namespace UdemyNLayerProject.API.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

       

        public CategoriesController(ICategoryService categoryService, IMapper mapper, RedisService redisService):base(redisService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
           

            // Biz startup içersine yazdığımız kodla mimariye sen ICategoryService türünden nesne ile karşılaşırsan
            // bize CategoryService nesnesi oluştur demiştik. Burada da bize CategoryService gelecektir. 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var redisDto = await _redisService.GetAsync<IEnumerable<CategoryDto>>($"categories");

            if (redisDto == null)
            {
                 redisDto = _mapper.Map<IEnumerable<CategoryDto>>(await _categoryService.GetAllAsync());
                _redisService.SetAsync($"categories", redisDto);
            }
            return Ok(redisDto);

            
        }

        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           CategoryDto redisDto = new CategoryDto();
           redisDto = await _redisService.GetAsync<CategoryDto>($"categories:{id}");

            if (redisDto == null)
            {
                redisDto = _mapper.Map<CategoryDto>(await _categoryService.GetByIdAsync(id));  
                _redisService.SetAsync($"categories:{id}", redisDto);
            }
            return Ok(redisDto);

        }

        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        [HttpGet("{id}/categories")]
        public async Task<IActionResult> GetWithProductsById(int id)
        {
            var categories = await _categoryService.GetWithProductsByIdAsync(id);
            return Ok(_mapper.Map<CategoryWithProductDto>(categories));

        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto model)
        {
            var newCategory = await _categoryService.AddAsync(_mapper.Map<Category>(model));
            model.Id = newCategory.Id;

            return Created("success", model);

        }

        [HttpPut]
        public IActionResult Update(CategoryUptadeDto model)
        {
            _categoryService.Update(_mapper.Map<Category>(model));

            return NoContent();

        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        public IActionResult Remove(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;

            _categoryService.Remove(category);

            return NoContent();
        }


    }
}

