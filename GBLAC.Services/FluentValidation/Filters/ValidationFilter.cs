using GBLAC.Services.FluentValidation.ErrorModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace GBLAC.Services.FluentValidation.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var modelStateErrors = context.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray());

                var errorResponse = new ErrorResponse();

                foreach (var kvp in modelStateErrors)
                {
                    foreach (var error in kvp.Value)
                    {
                        var errorModel = new ErrorModel.ErrorModel { FieldName = kvp.Key, Message = error };
                        errorResponse.Errors.Add(errorModel);
                    }
                }
                context.Result = new BadRequestObjectResult(errorResponse);
            }
            else
            {
                await next();
            }            
        }
    }
}
