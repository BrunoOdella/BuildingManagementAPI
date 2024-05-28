using CustomExceptions;
using CustomExceptions.InvitationExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BuildingManagementApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred.";

            if (context.Exception is UnauthorizedAccessException)
            {
                status = HttpStatusCode.Unauthorized;
                message = "Access is denied.";
            }
            else if (context.Exception is ArgumentException)
            {
                status = HttpStatusCode.BadRequest;
                message = context.Exception.Message;
            }
            else if (context.Exception is InvalidOperationException)
            {
                status = HttpStatusCode.NotFound;
                message = context.Exception.Message;
            }
            else if (context.Exception is EmailAlreadyExistsException)
            {
                status = HttpStatusCode.Conflict;
                message = context.Exception.Message;
            }
            else if (context.Exception is InvitationNotFoundException)
            {
                status = HttpStatusCode.NotFound;
                message = context.Exception.Message;
            }
            else if (context.Exception is InvitationAlreadyAcceptedException)
            {
                status = HttpStatusCode.Conflict;
                message = context.Exception.Message;
            }
            else if (context.Exception is InvitationExpiredException)
            {
                status = HttpStatusCode.Gone;
                message = context.Exception.Message;
            }
            else if (context.Exception is CannotDeleteAcceptedInvitationException)
            {
                status = HttpStatusCode.Conflict;
                message = context.Exception.Message;
            }
            else if (context.Exception is LocationAlreadyExistsException)
            {
                status = HttpStatusCode.Conflict;
                message = context.Exception.Message;
            }
            else if (context.Exception is InvalidCredentialsException)
            {
                status = HttpStatusCode.Unauthorized;
                message = context.Exception.Message;
            }

            context.Result = new ObjectResult(new { message })
            {
                StatusCode = (int)status
            };

            context.ExceptionHandled = true;
        }
    }
}
