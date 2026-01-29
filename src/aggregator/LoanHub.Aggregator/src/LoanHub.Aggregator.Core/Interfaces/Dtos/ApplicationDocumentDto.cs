namespace LoanHub.Aggregator.Core.Interfaces.Dtos;

public sealed record ApplicationDocumentDto(Stream Content, string ContentType, string FileName);