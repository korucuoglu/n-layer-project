using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.API.DTOs;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Service.Services;

namespace UdemyNLayerProject.API.Filters
{
    public class NotFoundFilter<TEntity> : IAsyncActionFilter where TEntity : class
    {
        private readonly IService<TEntity> _service;

        public NotFoundFilter(IService<TEntity> service)
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

                errorDto.Status = 404;

                errorDto.Errors.Add($"id'si {id} olan ürün veritabanında bulunamadı");

                context.Result = new NotFoundObjectResult(errorDto);

            }

        }
    }
}