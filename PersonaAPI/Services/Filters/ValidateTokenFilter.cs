using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using PersonaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace PersonaAPI.Services.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class ValidateTokenFilter : Attribute, IActionFilter
    {
        private readonly string _Token;

        public ValidateTokenFilter(IOptions<TokenSettings> tokenSettings)
        {
            _Token = tokenSettings.Value.APIToken;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string requestToken = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            // Con esto se refuerza la seguridad, verificando que el token no sea nulo o vacío
            if (String.IsNullOrEmpty(_Token) || requestToken.Trim() != _Token) {
                context.Result = new UnauthorizedResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No requiere implementación
        }
    }
}
