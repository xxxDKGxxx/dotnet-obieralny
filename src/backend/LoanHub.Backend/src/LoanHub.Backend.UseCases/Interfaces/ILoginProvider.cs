using LoanHub.Backend.UseCases.Features.Authentication.Login;

namespace LoanHub.Backend.UseCases.Interfaces;

public interface ILoginProvider
{
	public LoginType Type { get; }
	public Task<ExternalUserDto> AuthenticateAsync(string token, CancellationToken cancellationToken = default);
}