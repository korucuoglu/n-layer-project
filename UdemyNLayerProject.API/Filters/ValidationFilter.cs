using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.API.DTOs;

namespace UdemyNLayerProject.API.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorDto errorDto = new ErrorDto();

                errorDto.Status = 400;

                IEnumerable<ModelError> modelErrors = context.ModelState.Values.SelectMany(i => i.Errors);

                modelErrors.ToList().ForEach(i =>
                {
                    errorDto.Errors.Add(i.ErrorMessage);

                });

                context.Result = new BadRequestObjectResult(errorDto);
            }
        }

    }
}
