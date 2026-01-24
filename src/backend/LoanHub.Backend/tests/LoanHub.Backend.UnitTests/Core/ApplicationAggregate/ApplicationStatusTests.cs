namespace LoanHub.Backend.UnitTests.Core.ApplicationAggregate;

public class ApplicationStatusTests
{
	private static readonly IEnumerable<ApplicationStatus> _allStatuses =
	[
		ApplicationStatus.Created,
		ApplicationStatus.AwaitingSignature,
		ApplicationStatus.Signed,
		ApplicationStatus.Granted,
		ApplicationStatus.AwaitingAmendments,
		ApplicationStatus.Rejected,
		ApplicationStatus.Withdrawn
	];

	[Theory]
	[MemberData(nameof(GetCreatedTransitions))]
	public void CreatedStatus_CanTransitionTo_ReturnsCorrectValue(string newStatusName, string byWhoName, bool expected)
	{
		// Arrange
		var currentStatus = ApplicationStatus.Created;
		var newStatus = ApplicationStatus.FromName(newStatusName);
		var byWho = UserRole.FromName(byWhoName);

		// Act
		var result = currentStatus.CanTransitionTo(newStatus, byWho);

		// Assert
		result.ShouldBe(expected);
	}

	public static TheoryData<string, string, bool> GetCreatedTransitions()
	{
		var data = new TheoryData<string, string, bool>
		{
			{ ApplicationStatus.AwaitingSignature.Name, UserRole.Employee.Name, true },
			{ ApplicationStatus.Withdrawn.Name, UserRole.User.Name, true },
			{ ApplicationStatus.AwaitingSignature.Name, UserRole.User.Name, false },
			{ ApplicationStatus.Withdrawn.Name, UserRole.Employee.Name, false }
		};

		foreach (var status in _allStatuses.Except([ApplicationStatus.AwaitingSignature, ApplicationStatus.Withdrawn]))
		{
			data.Add(status.Name, UserRole.Employee.Name, false);
			data.Add(status.Name, UserRole.User.Name, false);
		}

		return data;
	}

	[Theory]
	[MemberData(nameof(GetAwaitingSignatureTransitions))]
	public void AwaitingSignatureStatus_CanTransitionTo_ReturnsCorrectValue(string newStatusName, string byWhoName, bool expected)
	{
		// Arrange
		var currentStatus = ApplicationStatus.AwaitingSignature;
		var newStatus = ApplicationStatus.FromName(newStatusName);
		var byWho = UserRole.FromName(byWhoName);

		// Act
		var result = currentStatus.CanTransitionTo(newStatus, byWho);

		// Assert
		result.ShouldBe(expected);
	}

	public static TheoryData<string, string, bool> GetAwaitingSignatureTransitions()
	{
		var data = new TheoryData<string, string, bool>
		{
			{ ApplicationStatus.Signed.Name, UserRole.User.Name, true },
			{ ApplicationStatus.Withdrawn.Name, UserRole.User.Name, true },
			{ ApplicationStatus.Signed.Name, UserRole.Employee.Name, false },
			{ ApplicationStatus.Withdrawn.Name, UserRole.Employee.Name, false }
		};

		foreach (var status in _allStatuses.Except([ApplicationStatus.Signed, ApplicationStatus.Withdrawn]))
		{
			data.Add(status.Name, UserRole.User.Name, false);
			data.Add(status.Name, UserRole.Employee.Name, false);
		}

		return data;
	}

	[Theory]
	[MemberData(nameof(GetSignedTransitions))]
	public void SignedStatus_CanTransitionTo_ReturnsCorrectValue(string newStatusName, string byWhoName, bool expected)
	{
		// Arrange
		var currentStatus = ApplicationStatus.Signed;
		var newStatus = ApplicationStatus.FromName(newStatusName);
		var byWho = UserRole.FromName(byWhoName);

		// Act
		var result = currentStatus.CanTransitionTo(newStatus, byWho);

		// Assert
		result.ShouldBe(expected);
	}

	public static TheoryData<string, string, bool> GetSignedTransitions()
	{
		var data = new TheoryData<string, string, bool>
		{
			{ ApplicationStatus.Granted.Name, UserRole.Employee.Name, true },
			{ ApplicationStatus.Rejected.Name, UserRole.Employee.Name, true },
			{ ApplicationStatus.AwaitingAmendments.Name, UserRole.Employee.Name, true },
			{ ApplicationStatus.Withdrawn.Name, UserRole.User.Name, true },
			{ ApplicationStatus.Granted.Name, UserRole.User.Name, false },
			{ ApplicationStatus.Rejected.Name, UserRole.User.Name, false },
			{ ApplicationStatus.AwaitingAmendments.Name, UserRole.User.Name, false },
			{ ApplicationStatus.Withdrawn.Name, UserRole.Employee.Name, false }
		};

		foreach (var status in _allStatuses.Except([ApplicationStatus.Granted, ApplicationStatus.Rejected, ApplicationStatus.AwaitingAmendments, ApplicationStatus.Withdrawn]))
		{
			data.Add(status.Name, UserRole.Employee.Name, false);
			data.Add(status.Name, UserRole.User.Name, false);
		}

		return data;
	}

	[Theory]
	[MemberData(nameof(GetAwaitingAmendmentsTransitions))]
	public void AwaitingAmendmentsStatus_CanTransitionTo_ReturnsCorrectValue(string newStatusName, string byWhoName, bool expected)
	{
		// Arrange
		var currentStatus = ApplicationStatus.AwaitingAmendments;
		var newStatus = ApplicationStatus.FromName(newStatusName);
		var byWho = UserRole.FromName(byWhoName);

		// Act
		var result = currentStatus.CanTransitionTo(newStatus, byWho);

		// Assert
		result.ShouldBe(expected);
	}

	public static TheoryData<string, string, bool> GetAwaitingAmendmentsTransitions()
	{
		var data = new TheoryData<string, string, bool>
		{
			{ ApplicationStatus.Signed.Name, UserRole.User.Name, true },
			{ ApplicationStatus.Withdrawn.Name, UserRole.User.Name, true },
			{ ApplicationStatus.Signed.Name, UserRole.Employee.Name, false },
			{ ApplicationStatus.Withdrawn.Name, UserRole.Employee.Name, false }
		};

		foreach (var status in _allStatuses.Except([ApplicationStatus.Signed, ApplicationStatus.Withdrawn]))
		{
			data.Add(status.Name, UserRole.User.Name, false);
			data.Add(status.Name, UserRole.Employee.Name, false);
		}

		return data;
	}

	[Fact]
	public void FinalStatuses_CannotTransitionToAnyOtherStatus()
	{
		var finalStatuses = new[]
		{
			ApplicationStatus.Granted,
			ApplicationStatus.Rejected,
			ApplicationStatus.Withdrawn
		};
		var allRoles = new[] { UserRole.User, UserRole.Employee, UserRole.Admin };

		foreach (var finalStatus in finalStatuses)
		{
			foreach (var status in _allStatuses)
			{
				foreach (var role in allRoles)
				{
					finalStatus.CanTransitionTo(status, role).ShouldBeFalse($"Should not be able to transition from {finalStatus.Value} to {status.Value} by {role.Value}");
				}
			}
		}
	}
}