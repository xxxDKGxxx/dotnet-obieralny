namespace LoanHub.Aggregator.Web.Endpoints.Applications.Post;

public class PostApplicationValidator : Validator<PostApplicationRequest>
{
	public PostApplicationValidator()
	{
		RuleFor(x => x.OfferId)
			.GreaterThanOrEqualTo(0);

		RuleFor(x => x.UserId)
			.GreaterThanOrEqualTo(0)
			.When(x =>
			{
				return x.UserId.HasValue;
			});

		RuleFor(x => x.Amount)
			.GreaterThan(0);

		RuleFor(x => x.Duration)
			.GreaterThan(0u);

		RuleFor(x => x.Financials)
			.NotNull()
			.ChildRules(financials =>
			{
				financials.RuleFor(x => x.Income)
					.GreaterThanOrEqualTo(0);
				financials.RuleFor(x => x.Costs)
					.GreaterThanOrEqualTo(0);
				financials.RuleFor(x => x.Dependents)
					.GreaterThanOrEqualTo(0);
				financials.RuleFor(x => x.Job)
					.NotEmpty();
			});

		RuleFor(x => x.Contact)
			.NotNull()
			.ChildRules(contact =>
			{
				contact.RuleFor(x => x.Email)
					.NotEmpty()
					.EmailAddress();
				contact.RuleFor(x => x.PhoneNumber)
					.NotEmpty();
				contact.RuleFor(x => x.Address)
					.NotEmpty();
			});

		RuleFor(x => x.PersonalData)
			.NotNull()
			.ChildRules(personal =>
			{
				personal.RuleFor(x => x.FirstName)
					.NotEmpty();
				personal.RuleFor(x => x.LastName)
					.NotEmpty();
				personal.RuleFor(x => x.Age)
					.GreaterThanOrEqualTo(18);
			});
	}
}