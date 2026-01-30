using LoanHub.Backend.UseCases.Features.Authentication.Login;

namespace LoanHub.Backend.UseCases.Features.Authentication.Test;

public sealed record TestLoginCommand(string Email) : IRequest<Result<TokenDto>>;