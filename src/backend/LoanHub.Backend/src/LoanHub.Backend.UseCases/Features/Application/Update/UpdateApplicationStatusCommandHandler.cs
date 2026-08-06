namespace LoanHub.Backend.UseCases.Features.Application.Update;

public class UpdateApplicationStatusCommandHandler(
	IRepository<ApplicationEntity> applicationsRepository,
	IReadRepository<UserEntity> usersRepository,
	IMapper mapper,
	IUpdateApplicationStatusService updateApplicationStatusService) :
	ICommandHandler<UpdateApplicationStatusCommand, Result<ApplicationDto>>
{
	public async Task<Result<ApplicationDto>> Handle(
		UpdateApplicationStatusCommand request,
		CancellationToken cancellationToken)
	{
		if (request.NewStatus == ApplicationStatus.AwaitingAmendments
			&& string.IsNullOrWhiteSpace(request.StatusChangeMessage))
		{
			return Result.Invalid(
				new ValidationError(
					"StatusChangeMessage",
					$"StatusChangeMessage is required when changing to "
					+ $"{ApplicationStatus.AwaitingAmendments.Value}"));
		}

		var application = await applicationsRepository.GetByIdAsync(request.ApplicationId, cancellationToken);

		if (application is null)
		{
			return Result.NotFound($"Application with {request.ApplicationId} id not found");
		}

		var requestingUser = await usersRepository.GetByIdAsync(request.RequestingUserId, cancellationToken);

		if (requestingUser is null)
		{
			return Result.NotFound($"User with {request.RequestingUserId} id not found");
		}

		var statusChangeMessage = request.StatusChangeMessage;

		try
		{
			application = await updateApplicationStatusService.UpdateStatusAsync(
				application,
				requestingUser.Role,
				request.NewStatus,
				statusChangeMessage,
				cancellationToken);

			return Result.Success(mapper.Map<ApplicationDto>(application));
		}
		catch (InvalidOperationException e)
		{
			return Result.Conflict(e.Message);
		}
	}
}