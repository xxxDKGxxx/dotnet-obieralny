namespace LoanHub.Backend.UseCases.Features.Authentication.Login;

public interface ILoginProvider
{
	LoginType Type { get; }
	Task<User> AuthenticateAsync(string token, CancellationToken cancellationToken = default);
}