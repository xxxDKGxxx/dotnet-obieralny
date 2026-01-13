// using LoanHub.Backend.Core.Interfaces;
//
// namespace LoanHub.Backend.Web.Endpoints;
//
// public sealed record EmailRequest(string ToEmail, string Subject, string Message);
//
// public sealed class EmailEndpoint(IEmailSender emailSender) : Endpoint<EmailRequest>
// {
// 	public override void Configure()
// 	{
// 		AllowAnonymous();
// 		Version(1);
// 		Post("/email");
// 	}
//
// 	public override async Task HandleAsync(EmailRequest req, CancellationToken ct)
// 	{
// 		await emailSender.SendEmailAsync(req.ToEmail, req.Subject, req.Message);
// 	}
// }