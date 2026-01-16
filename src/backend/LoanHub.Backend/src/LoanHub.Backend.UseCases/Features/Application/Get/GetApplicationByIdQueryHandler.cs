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

		if (user is not null)
		{
			if (application is not null)
			{
				if (application.UserId is not null)
				{
					if (user.Id == application.UserId)
					{
						return Result.Success(mapper.Map<ApplicationDto>(application));
					}
				}
				if (user.Role == UserRole.Admin || user.Role == UserRole.Employee)
				{
					return Result.Success(mapper.Map<ApplicationDto>(application));
				}
				return Result.Unauthorized();
			}
			return Result.NotFound();
		}
		return Result.Unauthorized();
	}
}