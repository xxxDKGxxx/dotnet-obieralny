
namespace LoanHub.Backend.UseCases.Features.CurrentUser.UpdateUser;

public sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<UserProfileDto>>
{
	private readonly IRepository<User> _userRepository;
	private readonly IMapper _mapper;
	private readonly ILogger<UpdateUserHandler> _logger;

	public UpdateUserHandler(IRepository<User> userRepository, IMapper mapper, ILogger<UpdateUserHandler> logger)
	{
		_userRepository = userRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<Result<UserProfileDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
		if (user is null)
		{
			_logger.LogWarning("User not found with ID: {UserId}", request.UserId);
			return Result.NotFound("User not found");
		}

		user.UpdateFirstName(request.Request.FirstName);
		user.UpdateLastName(request.Request.LastName);
		user.UpdateAddress(request.Request.Address);
		user.UpdatePhone(request.Request.Phone);
		user.UpdateJob(request.Request.Job);
		user.UpdateIncome(request.Request.Income);
		user.UpdateCosts(request.Request.Costs);
		user.UpdateAge(request.Request.Age);
		user.UpdateDependents(request.Request.Dependents);

		await _userRepository.UpdateAsync(user, cancellationToken);

		var userDto = _mapper.Map<UserProfileDto>(user);
		_logger.LogInformation("Updated user info for: {UserId}", user.Id);
		return Result.Success(userDto);
	}
}