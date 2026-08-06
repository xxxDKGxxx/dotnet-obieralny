using LoanHub.Backend.Core.Interfaces;

namespace LoanHub.Backend.Infrastructure.Email;

public sealed class FakeEmailSender(ILogger<FakeEmailSender> logger) : IEmailSender
{
	private readonly ILogger<FakeEmailSender> _logger = logger;

	public Task SendEmailAsync(string toEmail, string subject, string message)
	{
		_logger.LogInformation(
			"Not actually sending an email to {to} from {from} with subject {subject}",
			toEmail,
			"ArdalisBank",
			subject);

		return Task.CompletedTask;
	}
}