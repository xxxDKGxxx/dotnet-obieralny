using LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;

namespace LoanHub.Backend.Core.Interfaces;

public interface INotificationService
{
	public Task NotifyApplicationCreatedAsync(Application application);
	public Task NotifyApplicationStatusChangedAsync(Application application, string? message);
}