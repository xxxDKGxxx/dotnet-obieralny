using LoanHub.Backend.Core.UserAggregate;

namespace LoanHub.Backend.Core.Interfaces;

public interface ITokenProvider
{
	public string GenerateToken(User user);
}