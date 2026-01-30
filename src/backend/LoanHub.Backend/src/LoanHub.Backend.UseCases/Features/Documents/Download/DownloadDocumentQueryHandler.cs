namespace LoanHub.Backend.UseCases.Features.Documents.Download;

public class DownloadDocumentQueryHandler(
	IReadRepository<ApplicationEntity> applicationsRepository,
	IReadRepository<UserEntity> usersRepository,
	IApplicationDocumentService applicationDocumentService) :
	IQueryHandler<DownloadDocumentQuery, Result<ApplicationDocumentDto>>
{
	public async Task<Result<ApplicationDocumentDto>> Handle(
		DownloadDocumentQuery request,
		CancellationToken cancellationToken)
	{
		var application = await applicationsRepository.GetByIdAsync(request.ApplicationId, cancellationToken);

		if (application is null)
		{
			return Result.NotFound($"Application with id {request.ApplicationId} not found");
		}

		if (application.DocumentId != request.DocumentId)
		{
			return Result.Conflict("Wrong document id provided for the application");
		}

		var requestingUser = await usersRepository.GetByIdAsync(request.RequestingUserId, cancellationToken);

		if (requestingUser is null)
		{
			return Result.NotFound($"User with id {request.RequestingUserId} not found");
		}

		if (application.UserId != requestingUser.Id && requestingUser.Role != UserRole.Employee)
		{
			return Result.Forbidden("Requesting user does not have permissions to download document "
			                        + "for this application");
		}

		var applicationDocument = await applicationDocumentService.DownloadAsync(
			request.DocumentId,
			cancellationToken);

		return Result.Success(applicationDocument);
	}
}