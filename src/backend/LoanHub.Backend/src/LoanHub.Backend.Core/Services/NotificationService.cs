using LoanHub.Backend.Core.Interfaces;

namespace LoanHub.Backend.Core.Services;

public sealed class NotificationService(IEmailSender emailSender) : INotificationService
{
	public void NotifyApplicationCreated(string applicantEmailAddress, string offerTitle, string applicantName)
	{
		emailSender.SendEmailAsync(
			applicantEmailAddress,
			"Utworzenie aplikacji",
			NotifyApplicationCreatedSuccessfullyEmail(offerTitle, applicantName));
	}

	private static string NotifyApplicationCreatedSuccessfullyEmail(string offerName, string applicantName)
	{
		return $"Cześć {applicantName},<br>udało ci się pomyślnie utworzyć aplikację w ArdalisBanku na ofertę {offerName}!<br>"
			   + $"Będziemy ci wysyłać powiadomienia przy zmianach jej statusu.<br><br>"
			   + $"Pozdrawiamy, zespół ArdalisBank";
	}
}