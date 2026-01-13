namespace LoanHub.Backend.UseCases.Features.CurrentUser.UpdateUser;

public sealed class UpdateUserCommand : IRequest<Result<UserProfileDto>>
{
	public int UserId { get; }
	public UpdateUserRequest Request { get; }

	public UpdateUserCommand(int userId, UpdateUserRequest request)
	{
		UserId = userId;
		Request = request;
	}
}