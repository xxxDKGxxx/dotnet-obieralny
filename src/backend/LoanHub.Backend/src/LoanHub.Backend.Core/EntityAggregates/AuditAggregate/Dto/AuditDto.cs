namespace LoanHub.Backend.Core.EntityAggregates.AuditAggregate.Dto;

public record AuditDto(
	int Id,
	string Method,
	string Path,
	string? Body,
	int StatusCode,
	long DurationMs,
	string? Error,
	string? HeadersJson,
	string QueryParamsJson,
	string ParamsJson,
	DateTime CreatedAt);