using LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate.DTO;
namespace LoanHub.Backend.UseCases.Features.Application.Get;

public sealed record GetApplicationByIdQuery(int OfferId) : IQuery<Result<ApplicationDTO>>;