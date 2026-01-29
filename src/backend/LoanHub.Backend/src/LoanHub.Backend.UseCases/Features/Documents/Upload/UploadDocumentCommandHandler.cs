namespace LoanHub.Backend.UseCases.Features.Documents.Upload;

public class UploadDocumentCommandHandler(
	IReadRepository<ApplicationEntity> applicationsRepository,
	IReadRepository<UserEntity> usersRepository,
	IApplicationDocumentService applicationDocumentService,
	IUpdateApplicationStatusService updateApplicationStatusService) :
	ICommandHandler<UploadDocumentCommand, Result>
{
	public async Task<Result> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
	{
		Validate(request);

		var application = await applicationsRepository.GetByIdAsync(request.ApplicationId, cancellationToken);

		if (application is null)
		{
			return Result.NotFound($"Did not find Application with id {request.ApplicationId}");
		}

		if (!application.Status.AcceptsDocumentUploads())
		{
			return Result.Conflict($"Application in {application.Status.Value} does not support document uploads");
		}

		if (request.DocumentId != application.DocumentId)
		{
			return Result.Conflict($"Application has a different documentId than provided in the request");
		}

		if (request.RequestingUserId is null && application.UserId is not null)
		{
			return Result.Conflict("Application is not an anonymous application. "
								   + "Uploading documents requires authenticated user");
		}

		if (request.RequestingUserId is not null)
		{
			var requestingUser = await usersRepository.GetByIdAsync(request.RequestingUserId.Value, cancellationToken);

			if (requestingUser is null)
			{
				return Result.NotFound($"Did not find the user with Id: {request.RequestingUserId}");
			}

			if (application.UserId != requestingUser.Id)
			{
				return Result.Forbidden("Tried uploading document for someone else's application");
			}
		} // else application.UserId is null then we allow uploads for anyone

		await applicationDocumentService.UploadAsync(
			request.Document,
			request.DocumentId,
			request.ContentType,
			cancellationToken);

		await updateApplicationStatusService.UpdateStatusAsync(
			application,
			UserRole.User,
			ApplicationStatus.Signed,
			cancellationToken: cancellationToken);

		return Result.Success();
	}

	private static void Validate(UploadDocumentCommand request)
	{
		if (request.RequestingUserId is not null)
		{
			Guard.Against.Negative(request.RequestingUserId.Value);
		}

		Guard.Against.Negative(request.ApplicationId);
		Guard.Against.Empty(request.DocumentId, nameof(request.DocumentId));
		Guard.Against.Empty(request.ContentType, nameof(request.ContentType));
		Guard.Against.Zero(request.Document.Length);
	}
}