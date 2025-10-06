namespace LoanHub.Backend.UseCases.Contributors.List;

/// <summary>
/// Represents a service that will actually fetch the necessary data
/// Typically implemented in Infrastructure
/// </summary>
public interface IListContributorsQueryService
{
	public Task<IEnumerable<ContributorDTO>> ListAsync();
}
