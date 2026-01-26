using LoanHub.Backend.Core.EntityAggregates.UserAggregate;

namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;

public abstract class ApplicationStatus(
	string name,
	string value) :
	SmartEnum<ApplicationStatus, string>(name, value)
{
	/// <summary>
	/// Client has applied for a loan
	/// </summary>
	public static readonly ApplicationStatus Created = new CreatedApplicationStatus();

	/// <summary>
	/// Bank employee has reviewed and accepted the application, email with document sent to client
	/// </summary>
	public static readonly ApplicationStatus AwaitingSignature = new AwaitingSignatureApplicationStatus();

	/// <summary>
	/// Client has sent back the signed document
	/// </summary>
	public static readonly ApplicationStatus Signed = new SignedApplicationStatus();

	/// <summary>
	/// Loan has been granted after signed document verification
	/// </summary>
	public static readonly ApplicationStatus Granted = new GrantedApplicationStatus();

	/// <summary>
	/// Client has to re-send a signed document after amendments
	/// </summary>
	public static readonly ApplicationStatus AwaitingAmendments = new AwaitingAmendmentsApplicationStatus();

	/// <summary>
	/// Application has been rejected by the bank
	/// </summary>
	public static readonly ApplicationStatus Rejected = new RejectedApplicationStatus();

	/// <summary>
	/// Client has withdrawn the application
	/// </summary>
	public static readonly ApplicationStatus Withdrawn = new WithdrawnApplicationStatus();

	public abstract bool CanTransitionTo(ApplicationStatus newStatus, UserRole byWho);

	private sealed class CreatedApplicationStatus() :
		ApplicationStatus(nameof(CreatedApplicationStatus), nameof(Created))
	{
		public override bool CanTransitionTo(ApplicationStatus newStatus, UserRole byWho)
		{
			return newStatus switch
			{
				_ when (newStatus == AwaitingSignature || newStatus == Rejected) && byWho == UserRole.Employee => true,
				_ when newStatus == Withdrawn && byWho == UserRole.User => true,
				_ => false
			};
		}
	}

	private sealed class AwaitingSignatureApplicationStatus() :
		ApplicationStatus(nameof(AwaitingSignatureApplicationStatus), nameof(AwaitingSignature))
	{
		public override bool CanTransitionTo(ApplicationStatus newStatus, UserRole byWho)
		{
			return newStatus switch
			{
				_ when (newStatus == Signed || newStatus == Withdrawn) && byWho == UserRole.User => true,
				_ => false
			};
		}
	}

	private sealed class SignedApplicationStatus() :
		ApplicationStatus(nameof(SignedApplicationStatus), nameof(Signed))
	{
		public override bool CanTransitionTo(ApplicationStatus newStatus, UserRole byWho)
		{
			return newStatus switch
			{
				_ when (newStatus == Granted
					   || newStatus == Rejected
					   || newStatus == AwaitingAmendments)
					&& byWho == UserRole.Employee => true,
				_ when newStatus == Withdrawn && byWho == UserRole.User => true,
				_ => false
			};
		}
	}

	private sealed class GrantedApplicationStatus() :
		ApplicationStatus(nameof(GrantedApplicationStatus), nameof(Granted))
	{
		public override bool CanTransitionTo(ApplicationStatus newStatus, UserRole byWho)
		{
			return false;
		}
	}

	private sealed class AwaitingAmendmentsApplicationStatus() :
	ApplicationStatus(nameof(AwaitingAmendmentsApplicationStatus), nameof(AwaitingAmendments))
	{
		public override bool CanTransitionTo(ApplicationStatus newStatus, UserRole byWho)
		{
			return newStatus switch
			{
				_ when (newStatus == Signed || newStatus == Withdrawn) && byWho == UserRole.User => true,
				_ => false
			};
		}
	}

	private sealed class RejectedApplicationStatus() :
		ApplicationStatus(nameof(RejectedApplicationStatus), nameof(Rejected))
	{
		public override bool CanTransitionTo(ApplicationStatus newStatus, UserRole byWho)
		{
			return false;
		}
	}

	private sealed class WithdrawnApplicationStatus() :
		ApplicationStatus(nameof(WithdrawnApplicationStatus), nameof(Withdrawn))
	{
		public override bool CanTransitionTo(ApplicationStatus newStatus, UserRole byWho)
		{
			return false;
		}
	}
}