namespace LoanHub.Backend.Core.Interfaces;

public interface INotificationService
{
	public void NotifyApplicationCreated(string applicantEmailAddress, string offerTitle, string applicantName);
}