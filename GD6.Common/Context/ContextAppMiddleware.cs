using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GD6.Common.Context
{
    public class ContextAppMiddleware
    {

        private readonly RequestDelegate _next;

        public ContextAppMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext httpContext, ContextApp context)
        {
            // recupera a Claim de Usuário
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId != null && !string.IsNullOrEmpty(userId.Value))
            {
                context.SetUserId(Convert.ToInt32(userId.Value));
            }

            var clientId = httpContext.User.FindFirst(ContextApp.ClaimCliente);

            if (clientId != null && !string.IsNullOrEmpty(clientId.Value))
            {
                context.SetCliente(Convert.ToInt32(clientId.Value));
            }

            await _next(httpContext);
        }
    }
}
