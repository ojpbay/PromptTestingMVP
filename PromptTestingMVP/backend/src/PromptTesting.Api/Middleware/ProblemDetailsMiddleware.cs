using System.Net;
using System.Text.Json;

namespace PromptTesting.Api.Middleware;

public class ProblemDetailsMiddleware
{
    private readonly RequestDelegate _next;
    public ProblemDetailsMiddleware(RequestDelegate next) => _next = next;
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { title = "Conflict", detail = ex.Message, status = 409 }));
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { title = "Server Error", detail = ex.Message, status = 500 }));
        }
    }
}