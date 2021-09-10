using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyNLayerProject.API.Filters;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Data.DTOs.Products;

namespace UdemyNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;


        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }



        // [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(_mapper.Map<ProductDto>(product));
        }

        // [ServiceFilter(typeof(NotFoundFilter<Product>))]
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

        // [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;

            _productService.Remove(product);

            return NoContent();
        }




    }
}
