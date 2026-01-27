namespace LoanHub.Backend.Core.Interfaces.ApplicationDocumentService;

public sealed record ApplicationDocumentDto(Stream Content, string ContentType, string FileName);