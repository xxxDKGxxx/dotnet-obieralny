// namespace LoanHub.Aggregator.FunctionalTests;
//
// [Collection("Sequential")]
// public class RedirectionTests(CustomWebApplicationFactory<Program> factory) :
// 	IClassFixture<CustomWebApplicationFactory<Program>>
// {
// 	[Fact]
// 	public async Task DefaultBankRedirectionMiddleware_WhenRequestIsNotToBeAggregated_ShouldRedirectToBankApi()
// 	{
// 		var client = factory.CreateClient();
//
// 		var defaultBankUrl = factory.Services
// 			.GetRequiredService<IConfiguration>()
// 			.GetSection("DefaultBankUrl")
// 			.Value;
//
// 		Guard.Against
// 			.Null(defaultBankUrl);
//
// 		var testSuccessMessage = "Test Success";
// 		var unknownTestEndpoint = "/unknown-test-endpoint";
//
// 		factory.DefaultBankApiMockHandler
// 			.SetupRequest(HttpMethod.Get, defaultBankUrl + unknownTestEndpoint)
// 			.ReturnsResponse(HttpStatusCode.OK, testSuccessMessage);
//
// 		var response = await client.GetAsync(unknownTestEndpoint);
//
// 		response.StatusCode.ShouldBe(HttpStatusCode.OK);
//
// 		var content = response.Content.ShouldNotBeNull();
// 		var contentString = await content.ReadAsStringAsync();
//
// 		contentString.ShouldBe(testSuccessMessage);
// 	}
// }