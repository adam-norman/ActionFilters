using ActionFilters.Contracts;
using ActionFilters.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFilters.ActionFilters
{
    public class ValidationEntityExistAttribute<T> : IActionFilter where T : class,IEntity
    {
        private readonly MovieContext dbcontext;

        public ValidationEntityExistAttribute(MovieContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var id = Guid.Empty;
            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (Guid)context.ActionArguments["id"];
               
            }
            else
            {
                context.Result = new BadRequestObjectResult("Invalid id parameter");
            }
            var entity = dbcontext.Set<T>().SingleOrDefault(x => x.Id.Equals(id));
            if (entity == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
            }
        }
    }
}
