namespace LoanHub.Backend.Web.Endpoints.Documents.Post;

public class UploadDocumentValidator : Validator<UploadDocumentRequest>
{
	private const string DocxMimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

	public UploadDocumentValidator()
	{
		RuleFor(request => request.ApplicationId).
			GreaterThanOrEqualTo(0);

		RuleFor(request => request.DocumentId).
			NotEmpty();

		RuleFor(request => request.Document).
			Cascade(CascadeMode.Stop)
			.NotNull()
				.WithMessage("Document is required.")
			.Must(file => file.Length > 0)
				.WithMessage("File cannot be empty.")
			.Must(BeAValidDocx)
				.WithMessage("Allow file extensions: .docx.");
	}

	private static bool BeAValidDocx(IFormFile file)
	{
		var extension = Path.GetExtension(file.FileName).
			ToLower();

		if (extension != ".docx")
		{
			return false;
		}

		return file.ContentType == DocxMimeType;
	}
}