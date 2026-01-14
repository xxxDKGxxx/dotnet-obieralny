namespace LoanHub.Backend.Core.EntityAggregates.ApplicationAggregate;

public class ApplicationStatus(
	string name,
	string value) :
	SmartEnum<ApplicationStatus, string>(name, value)
{
	public static readonly ApplicationStatus Created = new CreatedApplicationStatus();
	public static readonly ApplicationStatus Submitted = new SubmittedApplicationStatus();
	public static readonly ApplicationStatus Reviewed = new ReviewedApplicationStatus();
	public static readonly ApplicationStatus AwaitingSignature = new AwaitingSignatureApplicationStatus();
	public static readonly ApplicationStatus Signed = new SignedApplicationStatus();
	public static readonly ApplicationStatus Granted = new GrantedApplicationStatus();
	public static readonly ApplicationStatus Rejected = new RejectedApplicationStatus();
	public static readonly ApplicationStatus Withdrawn = new WithdrawnApplicationStatus();

	private sealed class CreatedApplicationStatus() :
		ApplicationStatus(nameof(CreatedApplicationStatus), nameof(Created))
	{
	}
	private sealed class SubmittedApplicationStatus() :
		ApplicationStatus(nameof(SubmittedApplicationStatus), nameof(Submitted))
	{
	}

	private sealed class ReviewedApplicationStatus() :
		ApplicationStatus(nameof(ReviewedApplicationStatus), nameof(Reviewed))
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

	private sealed class RejectedApplicationStatus() :
		ApplicationStatus(nameof(RejectedApplicationStatus), nameof(Rejected))
	{
	}

	private sealed class WithdrawnApplicationStatus() :
		ApplicationStatus(nameof(WithdrawnApplicationStatus), nameof(Withdrawn))
	{
	}
}