using LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;
using LoanHub.Backend.Core.Interfaces;

namespace LoanHub.Backend.Core.Services;

public sealed class NotificationService(IEmailSender emailSender) : INotificationService
{
	public async Task NotifyApplicationCreatedAsync(
		string applicantEmailAddress,
		string offerTitle,
		string applicantName)
	{
		await emailSender.SendEmailAsync(
			applicantEmailAddress,
			"Utworzenie aplikacji",
			NotifyApplicationCreatedSuccessfullyEmail(offerTitle, applicantName));
	}

	public async Task NotifyApplicationStatusChangedAsync(
		int applicationId,
		string applicantEmailAddress,
		string offerTitle,
		string applicantName,
		ApplicationStatus newStatus,
		string? message)
	{
		await emailSender.SendEmailAsync(
			applicantEmailAddress,
			$"Zmiana statusu aplikacji [ID: {applicationId}]",
			NotifyStatusChangedEmail(applicationId, offerTitle, applicantName, newStatus, message));
	}

	private static string NotifyStatusChangedEmail(
		int applicationId,
		string offerTitle,
		string applicantName,
		ApplicationStatus newStatus,
		string? message)
	{
		var emailContent = $"Cześć {applicantName},<br>"
		                   + $"status twojej aplikacji o id {applicationId} na ofertę {offerTitle} został zmieniony"
		                   + $"na {newStatus.Value}. <br><br>";

		if (message is not null)
		{
			emailContent += $"Wiadomość dołączona do zmiany statusu:<br>{message}<br><br>";
		}

		emailContent += "Pozdrawiamy, zespół ArdalisBank";
		return emailContent;
	}

	private static string NotifyApplicationCreatedSuccessfullyEmail(string offerName, string applicantName)
	{
		return $"Cześć {applicantName},<br>udało ci się pomyślnie utworzyć aplikację w ArdalisBanku na ofertę {offerName}!<br>"
			   + $"Będziemy ci wysyłać powiadomienia przy zmianach jej statusu.<br><br>"
			   + $"Pozdrawiamy, zespół ArdalisBank";
	}
}