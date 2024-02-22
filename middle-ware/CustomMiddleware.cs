namespace GraphQL_API_X_clone.middle_ware;

public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Process the request and set the response headers
        // For example, you can allow any method, header, and protocol
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "*");

        await _next(context);
    }
}

