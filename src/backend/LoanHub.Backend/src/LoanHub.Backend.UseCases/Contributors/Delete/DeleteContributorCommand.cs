namespace LoanHub.Backend.UseCases.Contributors.Delete;

public record DeleteContributorCommand(int ContributorId) : ICommand<Result>;
