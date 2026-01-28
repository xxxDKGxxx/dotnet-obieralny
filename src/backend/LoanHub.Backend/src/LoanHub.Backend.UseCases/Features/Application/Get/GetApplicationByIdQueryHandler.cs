using System.Diagnostics.CodeAnalysis;

namespace LoanHub.Backend.UseCases.Features.Application.Get;

public sealed class GetApplicationByIdQueryHandler(
	IReadRepository<ApplicationEntity> applicationsRepository,
	IReadRepository<UserEntity> usersRepository,
	IMapper mapper) :
	IQueryHandler<GetApplicationByIdQuery, Result<ApplicationDto>>
{
	public async Task<Result<ApplicationDto>> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
	{
		var application = await applicationsRepository.GetByIdAsync(request.ApplicationId, cancellationToken);

		var user = await usersRepository.GetByIdAsync(request.RequestingUserId, cancellationToken);

		if (application is null)
		{
			return Result.NotFound();
		}

		if (user is null)
		{
			return Result.Unauthorized();
		}

		if ((application.UserId is null
			    || (application.UserId is not null
			        && application.UserId != user.Id))
		    && user.Role == UserRole.User)
		{
			return Result.Forbidden();
		}

		return Result.Success(mapper.Map<ApplicationDto>(application));
	}
}