namespace LoanHub.Backend.Web.Endpoints.Offers.Get;

public class GetCalculated : Endpoint<GetCalculatedOfferRequest, Result<CalculatedOfferDto>>
{
	public override void Configure()
	{
		AllowAnonymous();
		Version(1);
		Get("/calculated-offers/{OfferId:int}");
	}
}