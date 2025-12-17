using LoanHub.Backend.Core.UserAggregate;

namespace LoanHub.Backend.Core.Interfaces;

public interface ITokenProvider
{
	string GenerateToken(User user);
}