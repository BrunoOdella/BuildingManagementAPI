﻿using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagementApi.Filters;

//este filtro es para ver si el pibe existe

public class AuthenticationFilter : Attribute, IAuthorizationFilter
{
    private readonly IServiceProvider _provider;

    public AuthenticationFilter(IServiceProvider provider)
    {
        _provider = provider;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var authenticationService = _provider.GetRequiredService<IAuthenticationService>();
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
                var tokenEncontrado = authenticationService.BuscarToken(token);
                VerifyToken(context, tokenEncontrado);
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

    private void VerifyToken(AuthorizationFilterContext context, Guid tokenEncontrado)
    {
        if (tokenEncontrado.Equals(Guid.Empty))
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
            context.HttpContext.Items.Add("userID", tokenEncontrado.ToString());
        }
    }
}