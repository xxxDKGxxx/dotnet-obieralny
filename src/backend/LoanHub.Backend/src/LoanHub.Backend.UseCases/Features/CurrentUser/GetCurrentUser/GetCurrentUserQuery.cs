namespace LoanHub.Backend.UseCases.Features.CurrentUser.GetCurrentUser;

public sealed record GetCurrentUserQuery(int UserId) : IRequest<Result<UserProfileDto>>;