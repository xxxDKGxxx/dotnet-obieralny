namespace LoanHub.Backend.UseCases.Features.Application.Update;

public record UpdateApplicationStatusCommand(
	int ApplicationId,
	ApplicationStatus NewStatus,
	int RequestingUserId,
	string? StatusChangeMessage) :
	ICommand<Result<ApplicationDto>>;