namespace LoanHub.Backend.UseCases.Features.User.Get;

public sealed record GetCurrentUserQuery(int UserId) : IRequest<Result<UserProfileDto>>;