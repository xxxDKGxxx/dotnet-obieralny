using LoanHub.Backend.Web.Endpoints.Shared;

namespace LoanHub.Backend.Web.Endpoints.Authentication;

public sealed class GoogleAuthResponse
{
	public required string AccessToken { get; init; }
	public required UserDto User { get; init; }
}