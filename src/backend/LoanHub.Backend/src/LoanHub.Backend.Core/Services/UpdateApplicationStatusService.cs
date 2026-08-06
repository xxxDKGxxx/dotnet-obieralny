using LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;
using LoanHub.Backend.Core.EntityAggregates.UserAggregate;
using LoanHub.Backend.Core.Interfaces;

namespace LoanHub.Backend.Core.Services;

public class UpdateApplicationStatusService(
	IRepository<Application> applicationsRepository,
	INotificationService notificationService) : IUpdateApplicationStatusService
{
	public async Task<Application> UpdateStatusAsync(
		Application application,
		UserRole requestingUserRole,
		ApplicationStatus newStatus,
		string? statusChangeMessage = null,
		CancellationToken cancellationToken = default)
	{
		if (requestingUserRole != UserRole.Employee)
		{
			statusChangeMessage = null;
		}

		application.SetStatus(newStatus, requestingUserRole);
		application.SetStatusChangeMessage(statusChangeMessage);

		await applicationsRepository.UpdateAsync(application, cancellationToken);
		await notificationService.NotifyApplicationStatusChangedAsync(application, statusChangeMessage);

		return application;
	}
}