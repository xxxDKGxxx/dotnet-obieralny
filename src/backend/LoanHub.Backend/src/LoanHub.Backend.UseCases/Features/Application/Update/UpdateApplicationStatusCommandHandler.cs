namespace LoanHub.Backend.UseCases.Features.Application.Update;

public class UpdateApplicationStatusCommandHandler(
	IRepository<ApplicationEntity> applicationsRepository,
	IReadRepository<UserEntity> usersRepository,
	IMapper mapper,
	INotificationService notificationService) :
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

		if (requestingUser.Role != UserRole.Employee)
		{
			statusChangeMessage = null;
		}

		try
		{
			application.SetStatus(request.NewStatus, requestingUser.Role);
			application.SetStatusChangeMessage(statusChangeMessage);
		}
		catch (InvalidOperationException e)
		{
			return Result.Conflict(e.Message);
		}

		await applicationsRepository.UpdateAsync(application, cancellationToken);
		await notificationService.NotifyApplicationStatusChangedAsync(application, statusChangeMessage);

		return Result.Success(mapper.Map<ApplicationDto>(application));
	}
}