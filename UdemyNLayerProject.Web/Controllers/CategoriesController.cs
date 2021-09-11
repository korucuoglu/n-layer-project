using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Data.DTOs.Categories;
using UdemyNLayerProject.Web.ApiService;
using UdemyNLayerProject.Web.Filters;

namespace UdemyNLayerProject.Web.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly IApiService<CategoryDto> _apiService;

        public CategoriesController(IHttpClientFactory iHttpClientFactory, IApiService<CategoryDto> categoryApiService
            )
        {
            categoryApiService.Method = "categories";
            categoryApiService.HttpClient = iHttpClientFactory.CreateClient("ApiServiceClient");
            _apiService = categoryApiService;

        }

        //private readonly GenericApiService _apiService;


        //public CategoriesController(GenericApiService apiService)
        //{
        //    _apiService = apiService;
        //}

        public  IActionResult Index()
        {

            return View("Main");
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            await _apiService.AddAsync(categoryDto);

            return RedirectToAction("Index");
        }

        //update/5
        [ServiceFilter(typeof(NotFoundFilter<CategoryDto>))]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _apiService.GetByIdAsync(id);

            CategoryUptadeDto dto = new CategoryUptadeDto
            {
                Id = category.Id,
                Name = category.Name,
            };

            return View(dto);

            //return View(_mapper.Map<CategoryUptadeDto>(category));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUptadeDto uptadeDto)
        {
            CategoryDto dto = new CategoryDto
            {
                Id = uptadeDto.Id,
                Name = uptadeDto.Name
            };
            await _apiService.Update(dto);
            return RedirectToAction("Index");
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundFilter<CategoryDto>))]
        public async Task<IActionResult> Delete(int id)
        {
            var sonuc = await _apiService.Remove(id);

            return RedirectToAction("Index");
        }
    }
}
