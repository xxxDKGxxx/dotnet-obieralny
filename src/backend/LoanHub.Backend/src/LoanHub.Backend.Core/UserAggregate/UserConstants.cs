namespace LoanHub.Backend.Core.UserAggregate;

public static class UserConstants
{
	public const int EmailMaxLength = 254;
	public const int FirstNameMaxLength = 50;
	public const int LastNameMaxLength = 100;
	public const int AddressMaxLength = 500;
	public const int PhoneMaxLength = 15;
	public const int JobMaxLength = 100;
	public const string MoneyColumnType = "decimal(18,2)";

	public const int MinAge = 18;
	public const int MaxAge = 120;
	public const int MaxDependents = 20;
	public const decimal MaxFinancialAmount = 999999999.99m;
}