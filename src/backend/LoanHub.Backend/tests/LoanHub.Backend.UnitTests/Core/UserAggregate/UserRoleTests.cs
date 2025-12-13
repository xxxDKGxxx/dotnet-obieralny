using LoanHub.Backend.Core.UserAggregate;

namespace LoanHub.Backend.UnitTests.Core.UserAggregate;
public sealed class ApplicationProviderTypeTests
{
	[Fact]
	public void User_WhenCreatedAndQueriedByValueAndName_ShouldReturnCorrectInstance()
	{
		var userType = UserRole.User;

		userType.ShouldNotBeNull();

		userType.Name.ShouldBe("UserRoleType");
		userType.Value.ShouldBe("User");

		var result1 = UserRole.FromName("UserRoleType");

		result1.ShouldBe(userType);

		var result2 = UserRole.FromValue("User");

		result2.ShouldBe(userType);
	}

	[Fact]
	public void Employee_WhenCreatedAndQueriedByValueAndName_ShouldReturnCorrectInstance()
	{
		var userType = UserRole.Employee;

		userType.ShouldNotBeNull();

		userType.Name.ShouldBe("EmployeeRoleType");
		userType.Value.ShouldBe("Employee");

		var result1 = UserRole.FromName("EmployeeRoleType");

		result1.ShouldBe(userType);

		var result2 = UserRole.FromValue("Employee");

		result2.ShouldBe(userType);
	}

	[Fact]
	public void Admin_WhenCreatedAndQueriedByValueAndName_ShouldReturnCorrectInstance()
	{
		var userType = UserRole.Admin;

		userType.ShouldNotBeNull();

		userType.Name.ShouldBe("AdminRoleType");
		userType.Value.ShouldBe("Admin");

		var result1 = UserRole.FromName("AdminRoleType");

		result1.ShouldBe(userType);

		var result2 = UserRole.FromValue("Admin");

		result2.ShouldBe(userType);
	}
}