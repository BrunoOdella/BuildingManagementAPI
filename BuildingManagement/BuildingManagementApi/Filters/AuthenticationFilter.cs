using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagementApi.Filters;

//este filtro es para ver si el pibe existe

public class AuthenticationFilter : Attribute, IAuthorizationFilter
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationFilter(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string headerToken = context.HttpContext.Request.Headers["Authorization"];
        if (headerToken is null)
        {
            context.Result = new ContentResult()
            {
                Content = "A token is required",
                StatusCode = 401
            };
        }
        else
        {
            try
            {
                Guid token = Guid.Parse(headerToken);
                VerifyToken(token, context);
            }
            catch (FormatException)
            {
                context.Result = new ContentResult()
                {
                    Content = "The token format is invalid",
                    StatusCode = 401
                };
            }
        }
    }

    private void VerifyToken(Guid token, AuthorizationFilterContext context)
    {
        var tokenEncontrado = _authenticationService.BuscarToken(token);

        if (tokenEncontrado == null)
        {
            context.Result = new ContentResult()
            {
                Content = "The token is invalid",
                StatusCode = 401
            };
        }
        else
        {
            // Si el token es válido, podrías agregar información adicional al HttpContext, como los detalles del usuario.
            context.HttpContext.Items.Add("userID", token.ToString());
        }
    }

}