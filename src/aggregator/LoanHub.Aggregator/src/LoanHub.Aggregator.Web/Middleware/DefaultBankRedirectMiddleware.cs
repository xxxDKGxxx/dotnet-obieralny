namespace LoanHub.Aggregator.Web.Middleware;

public sealed class DefaultBankRedirectMiddleware(
	HttpClient httpClient,
	ILogger<DefaultBankRedirectMiddleware> logger) : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		if (context.GetEndpoint() is not null)
		{
			await next(context);
			return;
		}

		var targetUrl = httpClient.BaseAddress
						+ context.Request.Path.ToString().TrimStart('/')
						+ context.Request.QueryString;

		logger.LogInformation(
			"Redirecting {GetDisplayUrl} to {TargetUrl}...",
			context.Request.GetDisplayUrl(),
			targetUrl);

		var requestMessage = new HttpRequestMessage
		{
			RequestUri = new Uri(targetUrl),
			Method = new HttpMethod(context.Request.Method)
		};

		foreach (var header in context.Request.Headers)
		{
			requestMessage.Headers.TryAddWithoutValidation(header.Key, [.. header.Value]);
		}

		if (context.Request.ContentLength > 0)
		{
			requestMessage.Content = new StreamContent(context.Request.Body);

			if (context.Request.Headers.TryGetValue("Content-Type", out var contentType))
			{
				requestMessage.Content.Headers.TryAddWithoutValidation("Content-Type", [.. contentType]);
			}
		}

		var responseMessage = await httpClient.SendAsync(
			requestMessage,
			HttpCompletionOption.ResponseHeadersRead,
			context.RequestAborted);

		context.Response.StatusCode = (int)responseMessage.StatusCode;

		foreach (var header in responseMessage.Headers)
		{
			context.Response.Headers[header.Key] = header.Value.ToArray();
		}

		foreach (var header in responseMessage.Content.Headers)
		{
			context.Response.Headers[header.Key] = header.Value.ToArray();
		}

		// Kestrel chunking headers, aggregator receives response in chunks, by rewriting them we corrupt data
		// (because Kestrel chunks already chunked data)
		context.Response.Headers.Remove("Transfer-Encoding");
		context.Response.Headers.Remove("Content-Length");
		context.Response.Headers.Remove("Connection");

		logger.LogInformation(
			"Redirection successful. Response Status Code: {StatusCode}",
			(int)responseMessage.StatusCode);

		await responseMessage.Content.CopyToAsync(context.Response.Body);
	}
}