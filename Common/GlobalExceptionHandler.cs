using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        HttpStatusCode statusCode;
        string message;

        switch (exception)
        {
            case UnauthorizedAccessException:
                message = "You do not have permission to access this resource.";
                statusCode = HttpStatusCode.Forbidden;
                break;

            case NotFoundException:
                message = exception.Message;
                statusCode = HttpStatusCode.NotFound;
                break;

            case InvalidOperationException:
                message = "The request could not be completed due to an invalid operation.";
                statusCode = HttpStatusCode.BadRequest;
                break;

            default:
                message = "An unexpected error occurred.";
                statusCode = HttpStatusCode.InternalServerError;
                break;
        }

        // Подготвяне на JSON отговор
        var errorDetails = new
        {
            StatusCode = (int)statusCode,
            Message = message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails), cancellationToken);
        return true;
    }
}
