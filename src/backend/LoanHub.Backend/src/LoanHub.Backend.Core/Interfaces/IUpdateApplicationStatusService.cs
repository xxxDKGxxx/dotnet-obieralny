using LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;
using LoanHub.Backend.Core.EntityAggregates.UserAggregate;

namespace LoanHub.Backend.Core.Interfaces;

public interface IUpdateApplicationStatusService
{
	public Task<Application> UpdateStatusAsync(
		Application application,
		UserRole requestingUserRole,
		ApplicationStatus newStatus,
		string? statusChangeMessage = null,
		CancellationToken cancellationToken = default);
}