using LoanHub.Backend.Core.Interfaces;

namespace ArdalisBank.Infrastructure.Email;

public sealed class FakeEmailSender(ILogger<FakeEmailSender> logger) : IEmailSender
{
	private readonly ILogger<FakeEmailSender> _logger = logger;

	public Task SendEmailAsync(string to, string from, string subject, string body)
	{
		_logger.LogInformation("Not actually sending an email to {to} from {from} with subject {subject}", to, from, subject);
		return Task.CompletedTask;
	}
}