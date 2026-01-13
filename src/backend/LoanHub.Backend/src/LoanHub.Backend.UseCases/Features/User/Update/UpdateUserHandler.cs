
namespace LoanHub.Backend.UseCases.Features.User.Update;

public sealed class UpdateUserHandler(
	IRepository<Core.EntityAggregates.UserAggregate.User> userRepository,
	IMapper mapper,
	ILogger<UpdateUserHandler> logger)
	: ICommandHandler<UpdateUserCommand, Result<UserProfileDto>>
{
	public async Task<Result<UserProfileDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
		if (user is null)
		{
			logger.LogWarning("User not found with ID: {UserId}", request.UserId);
			return Result.NotFound("User not found");
		}

		user.UpdateFirstName(request.FirstName);
		user.UpdateLastName(request.LastName);
		user.UpdateAddress(request.Address);
		user.UpdatePhone(request.Phone);
		user.UpdateJob(request.Job);
		user.UpdateIncome(request.Income);
		user.UpdateCosts(request.Costs);
		user.UpdateAge(request.Age);
		user.UpdateDependents(request.Dependents);

		await userRepository.UpdateAsync(user, cancellationToken);

		var userDto = mapper.Map<UserProfileDto>(user);
		logger.LogInformation("Updated user info for: {UserId}", user.Id);
		return Result.Success(userDto);
	}
}