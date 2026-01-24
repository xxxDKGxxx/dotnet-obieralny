namespace LoanHub.Backend.UseCases.Features.Application.List;

public class ListApplicationsQueryHandler(
	IReadRepository<ApplicationEntity> applicationsRepository,
	IReadRepository<UserEntity> usersRepository,
	IMapper mapper) :
	IQueryHandler<ListApplicationsQuery, Result<IEnumerable<ApplicationDto>>>
{
	public async Task<Result<IEnumerable<ApplicationDto>>> Handle(
		ListApplicationsQuery request,
		CancellationToken cancellationToken)
	{
		var requestingUser = await usersRepository.GetByIdAsync(request.RequestingUserId, cancellationToken);

		if (requestingUser is null)
		{
			return Result.NotFound("Requesting user not found");
		}

		if (request.UserId is null && requestingUser.Role == UserRole.Employee)
		{
			var allApplications = await applicationsRepository.ListAsync(cancellationToken);
			return Result.Success(allApplications.Select(mapper.Map<ApplicationDto>));
		}

		if (request.UserId is null
			|| (request.UserId != request.RequestingUserId && requestingUser.Role != UserRole.Employee))
		{
			return Result.Forbidden();
		}

		var specification = new ApplicationsByUserIdSpec(request.UserId.Value);
		var userApplications = await applicationsRepository.ListAsync(
			specification,
			cancellationToken);
		return Result.Success(userApplications.Select(mapper.Map<ApplicationDto>));
	}
}