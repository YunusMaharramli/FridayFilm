using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace FridayFilm.WebApi.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Əgər gələn datada (DTO-da) hər hansı bir validator xətası varsa:
            if (!context.ModelState.IsValid)
            {
                // Xətaları toplayıb səliqəli bir formata salırıq
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                    );

                var errorResponse = new
                {
                    Message = "Göndərilən məlumatlarda xəta var.",
                    Errors = errorsInModelState
                };

                // Sorğunu dayandırıb istifadəçiyə 400 Bad Request qaytarırıq
                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            // Əgər heç bir xəta yoxdursa, sorğu Controller-ə doğru davam edir
            await next();
        }
    }
}