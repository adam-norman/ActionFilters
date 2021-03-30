using ActionFilters.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFilters.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
       
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.FirstOrDefault(i => i.Value is IEntity);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
