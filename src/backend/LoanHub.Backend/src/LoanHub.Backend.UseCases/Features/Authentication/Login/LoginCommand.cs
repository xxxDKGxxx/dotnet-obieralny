namespace LoanHub.Backend.UseCases.Features.Authentication.Login;

public sealed record LoginCommand(LoginType Type, string Token) : IRequest<Result<LoginResult>>;