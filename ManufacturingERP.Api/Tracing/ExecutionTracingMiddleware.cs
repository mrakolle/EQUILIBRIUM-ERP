public class ExecutionTracingMiddleware
{
    private readonly RequestDelegate _next;

    public ExecutionTracingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ExecutionContext trace)
    {
        trace.Step($"HTTP {context.Request.Method} {context.Request.Path}");

        await _next(context);

        trace.Step("HTTP REQUEST COMPLETED");
        trace.Dump();
    }
}