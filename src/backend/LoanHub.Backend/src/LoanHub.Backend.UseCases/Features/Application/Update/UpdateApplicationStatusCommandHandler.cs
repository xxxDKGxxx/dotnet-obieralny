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

		try
		{
			application.SetStatus(request.NewStatus, requestingUser.Role);
		}
		catch (InvalidOperationException e)
		{
			return Result.Conflict(e.Message);
		}

		await applicationsRepository.UpdateAsync(application, cancellationToken);

		// TODO add message handling in the future
		await notificationService.NotifyApplicationStatusChangedAsync(application, null);

		return Result.Success(mapper.Map<ApplicationDto>(application));
	}
}