namespace LoanHub.Backend.Core.UserAggregate;

public class UserRole(
	string name,
	string value) :
	SmartEnum<UserRole, string>(name, value)
{
	public static readonly UserRole User = new UserRoleType();
	public static readonly UserRole Employee = new EmployeeRoleType();
	public static readonly UserRole Admin = new AdminRoleType();

	private sealed class UserRoleType() :
		UserRole(nameof(UserRoleType), nameof(User))
	{
	}

	private sealed class EmployeeRoleType() :
		UserRole(nameof(EmployeeRoleType), nameof(Employee))
	{
	}

	private sealed class AdminRoleType() :
		UserRole(nameof(AdminRoleType), nameof(Admin))
	{
	}
}