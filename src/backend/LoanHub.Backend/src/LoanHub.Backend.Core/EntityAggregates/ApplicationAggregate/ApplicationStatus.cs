namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;

public class ApplicationStatus(
	string name,
	string value) :
	SmartEnum<ApplicationStatus, string>(name, value)
{
	/// <summary>
	/// Client has applied for a loan
	/// </summary>
	public static readonly ApplicationStatus Submitted = new SubmittedApplicationStatus();

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

	private sealed class SubmittedApplicationStatus() :
		ApplicationStatus(nameof(SubmittedApplicationStatus), nameof(Submitted))
	{
	}

	private sealed class AwaitingSignatureApplicationStatus() :
		ApplicationStatus(nameof(AwaitingSignatureApplicationStatus), nameof(AwaitingSignature))
	{
	}

	private sealed class SignedApplicationStatus() :
		ApplicationStatus(nameof(SignedApplicationStatus), nameof(Signed))
	{
	}

	private sealed class GrantedApplicationStatus() :
		ApplicationStatus(nameof(GrantedApplicationStatus), nameof(Granted))
	{
	}

	private sealed class AwaitingAmendmentsApplicationStatus() :
	ApplicationStatus(nameof(AwaitingAmendmentsApplicationStatus), nameof(AwaitingAmendments))
	{
	}

	private sealed class RejectedApplicationStatus() :
		ApplicationStatus(nameof(RejectedApplicationStatus), nameof(Rejected))
	{
	}

	private sealed class WithdrawnApplicationStatus() :
		ApplicationStatus(nameof(WithdrawnApplicationStatus), nameof(Withdrawn))
	{
	}
}