namespace LoanHub.Backend.UseCases.Features.Documents;

public class DownloadDocumentTemplateQueryHandler(IApplicationDocumentService applicationDocumentService) :
	IQueryHandler<DownloadDocumentTemplateQuery, Result<ApplicationDocumentDto>>
{
	public async Task<Result<ApplicationDocumentDto>> Handle(
		DownloadDocumentTemplateQuery request,
		CancellationToken cancellationToken)
	{
		var response = await applicationDocumentService.DownloadAsync(
			request.templateName,
			cancellationToken);

		return Result.Success(response);
	}
}