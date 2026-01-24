using LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;

namespace LoanHub.Backend.Core.Interfaces;

public interface INotificationService
{
	public Task NotifyApplicationCreatedAsync(
		string applicantEmailAddress,
		string offerTitle,
		string applicantName);
	public Task NotifyApplicationStatusChangedAsync(
		int applicationId,
		string applicantEmailAddress,
		string offerTitle,
		string applicantName,
		ApplicationStatus newStatus,
		string? message);
}