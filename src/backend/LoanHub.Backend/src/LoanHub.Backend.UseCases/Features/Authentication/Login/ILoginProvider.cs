namespace LoanHub.Backend.UseCases.Features.Authentication.Login;

public interface ILoginProvider
{
	public LoginType Type { get; }
	public Task<User> AuthenticateAsync(string token, CancellationToken cancellationToken = default);
}