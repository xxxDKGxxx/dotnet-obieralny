using LoanHub.Backend.Core.EntityAggregates.UserAggregate;

namespace LoanHub.Backend.Web.Endpoints.User.Update;

public class UpdateUserRequestValidator : Validator<UpdateUserRequest>
{
	public UpdateUserRequestValidator()
	{
		RuleFor(x => x.FirstName)
			.MaximumLength(UserConstants.FirstNameMaxLength)
			.NotEmpty()
			.NotNull();

		RuleFor(x => x.LastName)
			.MaximumLength(UserConstants.LastNameMaxLength)
			.NotEmpty()
			.NotNull();

		RuleFor(x => x.Address)
			.MaximumLength(UserConstants.AddressMaxLength)
			.NotEmpty();

		RuleFor(x => x.Phone)
			.MaximumLength(UserConstants.PhoneMaxLength)
			.Matches(@"^[0-9]{9}$")
			.When(x =>
			{
				return !string.IsNullOrEmpty(x.Phone);
			});

		RuleFor(x => x.Job)
			.MaximumLength(UserConstants.JobMaxLength)
			.When(x =>
			{
				return !string.IsNullOrEmpty(x.Job);
			});

		RuleFor(x => x.Age)
			.NotEmpty()
			.NotNull()
			.InclusiveBetween(UserConstants.MinAge, UserConstants.MaxAge);

		RuleFor(x => x.Income)
			.GreaterThanOrEqualTo(0)
			.When(x =>
			{
				return x.Income.HasValue;
			});

		RuleFor(x => x.Costs)
			.GreaterThanOrEqualTo(0)
			.LessThan(x => x.Income)
			.When(x =>
			{
				return x.Costs.HasValue;
			});

		RuleFor(x => x.Dependents)
			.InclusiveBetween(0, UserConstants.MaxDependents)
			.When(x =>
			{
				return x.Dependents.HasValue;
			});
	}
}