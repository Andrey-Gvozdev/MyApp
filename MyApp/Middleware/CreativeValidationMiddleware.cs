using OpenQA.Selenium;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Middleware;
public class CreativeValidationMiddleware
{
    private readonly RequestDelegate next;

    public CreativeValidationMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await this.next.Invoke(httpContext);
        }
        catch (ValidationException validationException)
        {
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsJsonAsync(validationException.Message);
        }
        catch (NotFoundException notFoundException)
        {
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsJsonAsync(notFoundException.Message);
        }
    }
}

public static class CreativeValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseCreativeValidationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CreativeValidationMiddleware>();
    }
}
