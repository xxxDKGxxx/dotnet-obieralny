namespace LoanHub.Backend.Web.Extensions;

public static class ResultExtensions
{
	public static async Task SendResult(this Result result, IEndpoint endpoint, CancellationToken ct = default)
	{
		var response = endpoint.HttpContext.Response;

		if (result.IsSuccess)
		{
			await response.SendOkAsync(cancellation: ct);
			return;
		}

		await HandleErrors(result, response, ct);
	}

	public static async Task SendResult<T>(this Result<T> result, IEndpoint endpoint, CancellationToken ct = default)
	{
		var response = endpoint.HttpContext.Response;

		if (result.IsSuccess)
		{
			await response.SendAsync(result.Value, 200, cancellation: ct);
			return;
		}

		await HandleErrors(result, response, ct);
	}

	private static async Task HandleErrors<T>(Result<T> result, HttpResponse response, CancellationToken ct)
	{
		switch (result.Status)
		{
			case ResultStatus.NotFound:
				await response.SendNotFoundAsync(cancellation: ct);
				break;

			case ResultStatus.Unauthorized:
				await response.SendUnauthorizedAsync(cancellation: ct);
				break;

			case ResultStatus.Forbidden:
				await response.SendForbiddenAsync(cancellation: ct);
				break;

			case ResultStatus.Invalid:
				await response.SendAsync(result.ValidationErrors, 400, cancellation: ct);
				break;

			case ResultStatus.Conflict:
				await response.SendAsync(result.Errors, 409, cancellation: ct);
				break;

			case ResultStatus.Error:
				await response.SendAsync(result.Errors, 400, cancellation: ct);
				break;

			case ResultStatus.CriticalError:
				await response.SendAsync(result.Errors, 500, cancellation: ct);
				break;

			case ResultStatus.Unavailable:
				await response.SendAsync(result.Errors, 503, cancellation: ct);
				break;

			default:
				await response.SendAsync(result.Errors, 500, cancellation: ct);
				break;
		}
	}
}