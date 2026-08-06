namespace LoanHub.Backend.UseCases.Features.Documents;

public record DownloadDocumentTemplateQuery(string templateName) : IQuery<Result<ApplicationDocumentDto>>;