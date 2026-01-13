using SendGrid;
using SendGrid.Helpers.Mail;

namespace LoanHub.Backend.Infrastructure.Email;

public sealed class SendGridEmailSender(IConfiguration configuration, ILogger<SendGridEmailSender> logger) : IEmailSender
{
	public async Task SendEmailAsync(string toEmail, string subject, string message)
	{
		var apiKey = configuration["SendGrid:ApiKey"];
		var client = new SendGridClient(apiKey);
		var from = new EmailAddress("ardalisbank8@gmail.com", "ArdalisBank");
		var to = new EmailAddress(toEmail);
		var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: message, htmlContent: message);

		var response = await client.SendEmailAsync(msg);

		if (response.IsSuccessStatusCode)
		{
			logger.LogInformation("Email sent successfully to {ToEmail}", toEmail);
		}
		else
		{
			logger.LogError("Email sent failed to {ToEmail}, Status Code: {StatusCode}"
			                + "Body {Body}", toEmail, response.StatusCode, response.Body.ReadAsStringAsync().Result);
		}
	}
}