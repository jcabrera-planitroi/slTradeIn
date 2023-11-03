using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace slTradeIn.Help;

public class InterceptorAttribute : ActionFilterAttribute, IActionFilter
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext actionContext,
        ActionExecutionDelegate next)
    {
        var signInManager = actionContext.HttpContext.RequestServices.GetService<SignInManager<IdentityUser>>();

        if (!actionContext.HttpContext.Session.Keys.Any() && signInManager != null)
        {
            actionContext.HttpContext.Session.Clear();

            foreach (var cookie in actionContext.HttpContext.Request.Cookies.Keys)
                actionContext.HttpContext.Response.Cookies.Delete(cookie);

            await signInManager.SignOutAsync();

            actionContext.Result =
                new RedirectResult(
                    "/Identity/Account/Login"); // Reemplaza con la URL de inicio de sesión que estés utilizando
        }
        else
        {
            await next();
        }
    }
}