using LoanHub.Backend.Core.EntityAggregates.UserAggregate;

namespace LoanHub.Backend.Core.Interfaces;

public interface ITokenProvider
{
	public string GenerateToken(User user);
	public Task<bool> ValidateTokenAsync(string token);
}