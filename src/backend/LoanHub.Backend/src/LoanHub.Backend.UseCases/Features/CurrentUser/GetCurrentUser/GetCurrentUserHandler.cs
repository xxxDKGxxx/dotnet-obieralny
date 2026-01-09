using LoanHub.Backend.Core.EntityAggregates.UserAggregate;

namespace LoanHub.Backend.UseCases.Features.CurrentUser.GetCurrentUser;

public sealed class GetCurrentUserHandler(
	IRepository<Core.EntityAggregates.UserAggregate.User> userRepository,
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

		var userDto = new UserProfileDto
		{
			Id = user.Id,
			Email = user.Email,
			FirstName = user.FirstName,
			LastName = user.LastName,
			Role = user.Role.Value,
			Address = user.Address,
			Phone = user.Phone,
			Job = user.Job,
			Income = user.Income,
			Costs = user.Costs,
			Age = user.Age,
			Dependents = user.Dependents
		};

		logger.LogInformation("Retrieved current user info for: {UserId}", user.Id);

		return Result.Success(userDto);
	}
}