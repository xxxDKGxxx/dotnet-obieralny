using LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;
using LoanHub.Backend.Core.EntityAggregates.OfferAggregate;
using LoanHub.Backend.Core.Interfaces;

namespace LoanHub.Backend.Core.Services;

public sealed class NotificationService(
	IEmailLinkProvider emailLinkProvider,
	IEmailSender emailSender,
	IReadRepository<Offer> offersRepository,
	ILogger<NotificationService> logger) : INotificationService
{
	private static readonly Dictionary<ApplicationStatus, string> _applicationStatusMap = new()
	{
		{ ApplicationStatus.Created, "Utworzona" },
		{ ApplicationStatus.AwaitingSignature, "Czeka na podpis" },
		{ ApplicationStatus.Signed, "Podpisana" },
		{ ApplicationStatus.Granted, "Zaakceptowana" },
		{ ApplicationStatus.AwaitingAmendments, "Czeka na poprawki" },
		{ ApplicationStatus.Rejected, "Odrzucona" },
		{ ApplicationStatus.Withdrawn, "Wycofana" }
	};

	public async Task NotifyApplicationCreatedAsync(Application application)
	{
		var offer = await offersRepository.GetByIdAsync(application.OfferId);

		if (offer is null)
		{
			logger.LogError("Could not send status change notification: "
							+ "Offer with id: {OfferId} not found", application.OfferId);
			return;
		}

		await emailSender.SendEmailAsync(
			application.ContactInfo.Email,
			"Utworzenie aplikacji",
			NotifyApplicationCreatedSuccessfullyEmail(offer.Title, application.PersonalData.FirstName));
	}

	public async Task NotifyApplicationStatusChangedAsync(
		Application application,
		string? message)
	{
		var offer = await offersRepository.GetByIdAsync(application.OfferId);

		if (offer is null)
		{
			logger.LogError("Could not send status change notification: "
							+ "Offer with id: {OfferId} not found", application.OfferId);
			return;
		}

		await emailSender.SendEmailAsync(
			application.ContactInfo.Email,
			$"Zmiana statusu aplikacji [ID: {application.Id}]",
			NotifyStatusChangedEmail(
				application.Id,
				offer.Title,
				application.PersonalData.FirstName,
				application.Status,
				message,
				application.DocumentId));
	}

	private string NotifyStatusChangedEmail(
		int applicationId,
		string offerTitle,
		string applicantName,
		ApplicationStatus newStatus,
		string? message,
		string? documentId)
	{
		var emailContent = $"Cześć {applicantName},<br>"
						   + $"status twojej aplikacji o id {applicationId} na ofertę {offerTitle} został zmieniony "
						   + $"na {_applicationStatusMap[newStatus]}. <br><br>";

		if (message is not null)
		{
			emailContent += $"Wiadomość dołączona do zmiany statusu:<br>{message}<br><br>";
		}

		if (newStatus.AcceptsDocumentUploads() && documentId is not null)
		{
			var uploadLink = emailLinkProvider.GetUploadLink(documentId, applicationId);
			var templateLink = emailLinkProvider.GetTemplateLink();

			emailContent += "Nowy status wymaga załączenia dokumentu. Dokument możesz przesłać pod adresem:<br>"
							+ $"<a href=\"{uploadLink}\">{uploadLink}</a><br><br>";

			emailContent += "Wzór dokumentu możesz pobrać pod adresem:<br>"
							+ $"<a href=\"{templateLink}\">{templateLink}</a><br><br>";
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