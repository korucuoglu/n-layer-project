using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Data.DTOs._Error;
using UdemyNLayerProject.Web.ApiService;

namespace UdemyNLayerProject.Web.Filters
{
    public class NotFoundFilter<TEntity> : IAsyncActionFilter where TEntity : class
    {
        private readonly IApiService<TEntity> _service;

        public NotFoundFilter(IApiService<TEntity> service)
        {
            _service = service;
            
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)

        {

            var id = Convert.ToInt32(context.RouteData.Values["id"].ToString());


            var entry = await _service.GetByIdAsync(id);

            if (entry != null)
            {
                await next();
            }

            else
            {
                ErrorDto errorDto = new ErrorDto();

                errorDto.Errors.Add($"id'si {id} olan kategori veritabanında bulunamadı");

                context.Result = new RedirectToActionResult("Error", "Home", errorDto);
            }


        }
    }
}
