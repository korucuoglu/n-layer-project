using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyNLayerProject.API.Filters;
using UdemyNLayerProject.API.Service;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Data.DTOs.Products;

namespace UdemyNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper, RedisService redisService): base(redisService)
        {
            _productService = productService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var redisDto = await _redisService.GetAsync<IEnumerable<ProductDto>>($"products");

            if (redisDto == null)
            {
                redisDto = _mapper.Map<IEnumerable<ProductDto>>(await _productService.GetAllAsync());
                _redisService.SetAsync($"products", redisDto);
            }
            return Ok(redisDto);
        }



        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ProductDto redisDto = new ProductDto();
            redisDto = await _redisService.GetAsync<ProductDto>($"products:{id}");

            if (redisDto == null)
            {
               redisDto = _mapper.Map<ProductDto>(await _productService.GetByIdAsync(id));
               _redisService.SetAsync($"products:{id}", redisDto);
            }
            return Ok(redisDto);
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategoryById(int id)
        {
            var product = await _productService.GetWithCatByIdAsync(id);
            return Ok(_mapper.Map<ProductWithCategoryDto>(product));

        }
       
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto model)
        {
            var newProduct = await _productService.AddAsync(_mapper.Map<Product>(model));

            return Created(string.Empty, _mapper.Map<ProductDto>(newProduct));

        }

        [HttpPut]
        public IActionResult Update(ProductUptadeDto model)
        {
            _productService.Update(_mapper.Map<Product>(model));

            return NoContent();

        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;

            _productService.Remove(product);

            return NoContent();
        }




    }
}
