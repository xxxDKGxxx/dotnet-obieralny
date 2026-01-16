namespace LoanHub.Backend.Core.EntityAggregates.AuditAggregate;

public sealed class Audit(
	string method,
	string path,
	string? body,
	int statusCode,
	long durationMs,
	string? error,
	string? headersJson,
	string queryParamsJson,
	string paramsJson) :
	LoanHubEntityBase,
	IAggregateRoot
{
	public string Method { get; private set; } = method;
	public string Path { get; private set; } = path;
	public string? Body { get; private set; } = body;
	public string? HeadersJson { get; private set; } = headersJson;
	public string ParamsJson {get; private set;} = paramsJson;
	public string QueryParamsJson { get; private set; } = queryParamsJson;
	public int StatusCode { get; private set; } = statusCode;
	public long DurationMs { get; private set; } = durationMs;
	public string? Error { get; private set; } = error;
}