namespace LoanHub.Backend.Web.Extensions;

public static class ResultExtensions
{
	public static async Task SendResult(this Result result, IEndpoint endpoint, CancellationToken ct = default)
	{
		if (result.IsSuccess)
		{
			await (endpoint as dynamic).SendOkAsync(ct);
			return;
		}

		switch (result.Status)
		{
			case ResultStatus.NotFound:
				await (endpoint as dynamic).SendNotFoundAsync(ct);
				break;

			case ResultStatus.Unauthorized:
				await (endpoint as dynamic).SendUnauthorizedAsync(ct);
				break;

			case ResultStatus.Forbidden:
				await (endpoint as dynamic).SendForbiddenAsync(ct);
				break;

			case ResultStatus.Invalid:
				await (endpoint as dynamic).SendAsync(result.ValidationErrors, 400, ct);
				break;

			case ResultStatus.Conflict:
				await (endpoint as dynamic).SendAsync(result.Errors, 409, ct);
				break;

			case ResultStatus.Error:
				await (endpoint as dynamic).SendAsync(result.Errors, 400, ct);
				break;

			case ResultStatus.CriticalError:
				await (endpoint as dynamic).SendAsync(result.Errors, 500, ct);
				break;

			case ResultStatus.Unavailable:
				await (endpoint as dynamic).SendAsync(result.Errors, 503, ct);
				break;

			default:
				await (endpoint as dynamic).SendAsync(result.Errors, 500, ct);
				break;
		}
	}

	public static async Task SendResult<T>(this Result<T> result, IEndpoint endpoint, CancellationToken ct = default)
	{
		if (result.IsSuccess)
		{
			await (endpoint as dynamic).SendOkAsync(result.Value, ct);
			return;
		}

		switch (result.Status)
		{
			case ResultStatus.NotFound:
				await (endpoint as dynamic).SendNotFoundAsync(ct);
				break;

			case ResultStatus.Unauthorized:
				await (endpoint as dynamic).SendUnauthorizedAsync(ct);
				break;

			case ResultStatus.Forbidden:
				await (endpoint as dynamic).SendForbiddenAsync(ct);
				break;

			case ResultStatus.Invalid:
				await (endpoint as dynamic).SendAsync(result.ValidationErrors, 400, ct);
				break;

			case ResultStatus.Conflict:
				await (endpoint as dynamic).SendAsync(result.Errors, 409, ct);
				break;

			case ResultStatus.Error:
				await (endpoint as dynamic).SendAsync(result.Errors, 400, ct);
				break;

			case ResultStatus.CriticalError:
				await (endpoint as dynamic).SendAsync(result.Errors, 500, ct);
				break;

			case ResultStatus.Unavailable:
				await (endpoint as dynamic).SendAsync(result.Errors, 503, ct);
				break;

			default:
				await (endpoint as dynamic).SendAsync(result.Errors, 500, ct);
				break;
		}
	}
}