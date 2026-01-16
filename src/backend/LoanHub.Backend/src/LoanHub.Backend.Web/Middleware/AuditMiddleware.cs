namespace LoanHub.Backend.Web.Middleware;

public class AuditMiddleware(IRepository<Audit> auditRepository) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
       var stopwatch = Stopwatch.StartNew();
       var requestBodyContent = "";
       var serializedQuery = JsonSerializer.Serialize(
            context.Request
	            .Query
	            .ToDictionary(
		            q => q.Key,
		            q => q.Value
			            .ToString()));

       var serializedHeaders = JsonSerializer.Serialize(
          context.Request
             .Headers
             .Where(m => !string.Equals(m.Key, "Cookie", StringComparison.OrdinalIgnoreCase)
                         && !string.Equals(m.Key, "Cookies", StringComparison.OrdinalIgnoreCase)
                         && !string.Equals(m.Key, "Authorization", StringComparison.OrdinalIgnoreCase)
                         && !string.Equals(m.Key, "Authentication", StringComparison.OrdinalIgnoreCase))
             .ToDictionary(m => m.Key, m => m.Value.ToString())
       );

       try
       {
          context.Request.EnableBuffering();

          using (var reader = new StreamReader(
              context.Request.Body,
              Encoding.UTF8,
              true,
              1024,
              true))
          {
             requestBodyContent = await reader.ReadToEndAsync();
             context.Request.Body.Position = 0;
          }

          await next(context);
       }
       catch (Exception ex)
       {
          stopwatch.Stop();

          await PrepareAndLog(context, requestBodyContent, stopwatch, serializedHeaders, serializedQuery, ex.Message);

          throw;
       }

       stopwatch.Stop();

       await PrepareAndLog(context, requestBodyContent, stopwatch, serializedHeaders, serializedQuery);
    }

    private async Task PrepareAndLog(
       HttpContext context,
       string requestBodyContent,
       Stopwatch stopwatch,
       string serializedHeaders,
       string serializedQuery,
       string? error = null)
    {
       var serializedRouteValues = JsonSerializer.Serialize(context.Request.RouteValues);

       await LogToDb(
           context,
           requestBodyContent,
           stopwatch,
           serializedHeaders,
           serializedQuery,
           serializedRouteValues,
           error);
    }

    private async Task LogToDb(
       HttpContext context,
       string requestBodyContent,
       Stopwatch stopwatch,
       string serializedHeaders,
       string serializedQuery,
       string serializedRouteValues,
       string? error = null)
    {

       var audit = new Audit(
          context.Request.Method,
          context.Request.Path,
          requestBodyContent,
          context.Response.StatusCode,
          stopwatch.ElapsedMilliseconds,
          error,
          serializedHeaders,
          serializedQuery,
          serializedRouteValues
       );

       await auditRepository.AddAsync(audit);
    }
}