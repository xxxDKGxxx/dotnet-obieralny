namespace LoanHub.Backend.UseCases.Features.CurrentUser.GetCurrentUser;

public sealed class GetCurrentUserHandler(
	IRepository<Core.EntityAggregates.UserAggregate.User> userRepository,
	IMapper mapper,
	ILogger<GetCurrentUserHandler> logger) : IRequestHandler<GetCurrentUserQuery, Result<UserProfileDto>>
{
	public async Task<Result<UserProfileDto>> Handle(
		GetCurrentUserQuery request,
		CancellationToken cancellationToken)
	{
		var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

		if (user is null)
		{
			logger.LogWarning("User not found with ID: {UserId}", request.UserId);
			return Result.NotFound("User not found");
		}

		var userDto = mapper.Map<UserProfileDto>(user);

		logger.LogInformation("Retrieved current user info for: {UserId}", user.Id);

		return Result.Success(userDto);
	}
}