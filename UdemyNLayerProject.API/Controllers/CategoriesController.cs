using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.API.DTOs;
using UdemyNLayerProject.API.Filters;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;

namespace UdemyNLayerProject.API.Controllers
{
    [ValidationFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;


        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;

            // Biz startup içersine yazdığımız kodla mimariye sen ICategoryService türünden nesne ile karşılaşırsan
            // bize CategoryService nesnesi oluştur demiştik. Burada da bize CategoryService gelecektir. 
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProductsById(int id)
        {
            var categories = await _categoryService.GetWithProductsByIdAsync(id);
            return Ok(_mapper.Map<CategoryWithProductDto>(categories));

        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto model)
        {
            var newCategory = await _categoryService.AddAsync(_mapper.Map<Category>(model));

            return Created(string.Empty, _mapper.Map<CategoryDto>(newCategory));


        }

        [HttpPut]
        public IActionResult Update(CategoryDto model)
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
