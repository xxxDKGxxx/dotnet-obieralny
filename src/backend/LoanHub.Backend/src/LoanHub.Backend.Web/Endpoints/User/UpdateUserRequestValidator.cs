namespace LoanHub.Backend.Web.Endpoints.User;

public class UpdateUserRequestValidator : Validator<UpdateUserRequest>
{
	public UpdateUserRequestValidator()
	{
		RuleFor(x => x.FirstName)
			.MaximumLength(50);

		RuleFor(x => x.LastName)
			.MaximumLength(50);

		RuleFor(x => x.Address)
			.MaximumLength(200);

		RuleFor(x => x.Phone)
			.MaximumLength(9)
			.Matches(@"^[0-9]{9}$")
			.When(x =>
			{
				return !string.IsNullOrEmpty(x.Phone);
			});

		RuleFor(x => x.Job)
			.MaximumLength(100);

		RuleFor(x => x.Age)
			.InclusiveBetween(18, 120)
			.When(x =>
			{
				return x.Age.HasValue;
			});

		RuleFor(x => x.Income)
			.GreaterThanOrEqualTo(0)
			.When(x =>
			{
				return x.Income.HasValue;
			});

		RuleFor(x => x.Costs)
			.GreaterThanOrEqualTo(0)
			.When(x =>
			{
				return x.Costs.HasValue;
			});

		RuleFor(x => x.Dependents)
			.GreaterThanOrEqualTo(0)
			.When(x =>
			{
				return x.Dependents.HasValue;
			});
	}
}