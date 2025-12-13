using LoanHub.Backend.Core.UserAggregate;

namespace LoanHub.Backend.Core.Interfaces;

public interface IJwtTokenService
{
	public string GenerateToken(User user);
	public Task<ClaimsPrincipal?> ValidateTokenAsync(string token);
}