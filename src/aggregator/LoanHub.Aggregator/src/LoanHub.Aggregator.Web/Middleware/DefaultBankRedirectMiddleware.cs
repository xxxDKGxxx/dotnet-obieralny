using Microsoft.AspNetCore.Http.Extensions;

namespace LoanHub.Aggregator.Web.Middleware;

public sealed class DefaultBankRedirectMiddleware(
	HttpClient httpClient,
	ILogger<DefaultBankRedirectMiddleware> logger,
	IConfiguration configuration) : IMiddleware
{
	private readonly string _defaultBankUrl = configuration.GetSection("DefaultBankUrl").Value
		?? throw new Exception("Default Bank Url was not defined");

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		var targetUrl = _defaultBankUrl + context.Request.Path + context.Request.QueryString;

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