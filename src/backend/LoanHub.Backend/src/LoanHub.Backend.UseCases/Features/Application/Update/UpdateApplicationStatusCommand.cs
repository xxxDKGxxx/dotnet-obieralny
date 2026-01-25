namespace LoanHub.Backend.UseCases.Features.Application.Update;

public record UpdateApplicationStatusCommand(int ApplicationId, ApplicationStatus NewStatus, int RequestingUserId) :
	ICommand<Result<ApplicationDto>>;