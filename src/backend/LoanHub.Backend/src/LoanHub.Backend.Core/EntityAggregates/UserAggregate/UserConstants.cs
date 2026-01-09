namespace LoanHub.Backend.Core.EntityAggregates.UserAggregate;

public static class UserConstants
{
	public const decimal MaxFinancialAmount = 999_999_999.99m;
	public const int MinAge = 18;
	public const int MaxAge = 120;
	public const int MaxDependents = 20;
	public const int EmailMaxLength = 254;
	public const int FirstNameMaxLength = 50;
	public const int LastNameMaxLength = 50;
	public const int AddressMaxLength = 200;
	public const int PhoneMaxLength = 9;
	public const int JobMaxLength = 100;
}