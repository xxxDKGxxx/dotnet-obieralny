namespace LoanHub.Backend.Web.Endpoints.Documents.Get;

public class GetTemplate(IMediator mediator, IConfiguration configuration) : EndpointWithoutRequest
{
	private readonly string _templateName = configuration.GetValue<string>("TemplateName") ??
											throw new Exception("Template name was not defined in the configuration");
	public override void Configure()
	{
		Version(1);
		AllowAnonymous();
		Get("/documents/template");
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		var query = new DownloadDocumentTemplateQuery(_templateName);

		var response = await mediator.Send(query, ct);

		if (response.IsSuccess)
		{
			await SendStreamAsync(
				response.Value.Content,
				response.Value.FileName,
				contentType: response.Value.ContentType,
				cancellation: ct);
			return;
		}

		await response.SendResult(this, ct: ct);
	}
}