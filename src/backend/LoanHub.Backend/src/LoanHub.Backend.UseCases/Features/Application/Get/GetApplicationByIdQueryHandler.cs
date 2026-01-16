namespace LoanHub.Backend.UseCases.Features.Application.Get;

public sealed class GetApplicationByIdQueryHandler(IReadRepository<ApplicationEntity> offersRepository, IMapper mapper) :
	IQueryHandler<GetApplicationByIdQuery, Result<ApplicationDto>>
{
	public async Task<Result<ApplicationDto>> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
	{
		var spec = new ApplicationByIdSpec(request.ApplicationId);
		var application = await offersRepository.SingleOrDefaultAsync(spec, cancellationToken);

		return application is null ? Result.NotFound() : Result.Success(mapper.Map<ApplicationDto>(application));
	}
}