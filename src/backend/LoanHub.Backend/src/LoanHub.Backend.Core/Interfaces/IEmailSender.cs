namespace LoanHub.Backend.Core.Interfaces;

public interface IEmailSender
{
	public Task SendEmailAsync(string toEmail, string subject, string message);
}