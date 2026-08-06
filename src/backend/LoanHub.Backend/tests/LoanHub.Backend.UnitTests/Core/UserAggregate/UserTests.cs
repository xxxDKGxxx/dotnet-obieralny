namespace LoanHub.Backend.UnitTests.Core.UserAggregate;

public class UserTests
{
	[Fact]
	public void User_WhenCreated_ShouldHaveCorrectFields()
	{

		const string email = "xyz@xyz.com";
		const string firstName = "John";
		const string lastName = "Doe";
		var role = UserRole.User;

		var user = new User(
			email,
			firstName,
			lastName,
			role);

		user.Email.ShouldBe(email);
		user.FirstName.ShouldBe(firstName);
		user.LastName.ShouldBe(lastName);
		user.Role.ShouldBe(role);
		user.Id.ShouldBe(0);
	}
}