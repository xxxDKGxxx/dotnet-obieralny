using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanHub.Backend.IntegrationTests.Data;
using LoanHub.Backend.UseCases.Features.Application.Get;
using Microsoft.EntityFrameworkCore;

namespace LoanHub.Backend.IntegrationTests.Features.Application.Get;
public class GetApplicationByIdQueryHandlerIntegrationTests : BaseEfRepoTestFixture
{
	private readonly EfRepository<ApplicationEntity> _applicationRepository;
	private readonly IMapper _mapper;
	private readonly GetApplicationByIdQueryHandler _handler;

	public GetApplicationByIdQueryHandlerIntegrationTests()
	{
		_applicationRepository = new EfRepository<ApplicationEntity>(_dbContext);

		var config = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile<ApplicationProfile>();
		}, new SerilogLoggerFactory());

		_mapper = config.CreateMapper();
		_handler = new GetApplicationByIdQueryHandler(_applicationRepository, _mapper);
	}

	[Fact]
	public async Task Handle_ShouldReturnCorrectApplication_WhenIdExists()
	{
		var app1 = CreateTestApplication(userId: 10);
		var app2 = CreateTestApplication(userId: 15);
		var app3 = CreateTestApplication(userId: 20);

		await _dbContext.Set<ApplicationEntity>().AddRangeAsync(app1, app2, app3);
		await _dbContext.SaveChangesAsync();

		var targetId = app2.Id;
		var query = new GetApplicationByIdQuery(targetId);

		var result = await _handler.Handle(query, CancellationToken.None);

		result.IsSuccess.ShouldBeTrue();
		result.Value.ShouldNotBeNull();
		result.Value.OfferId.ShouldBe(app2.OfferId);
		result.Value.UserId.ShouldBe(app2.UserId);
		result.Value.Status.ShouldBe(ApplicationStatus.Created.Name);
		result.Value.FirstName.ShouldBe(app2.PersonalData.FirstName);
		result.Value.LastName.ShouldBe(app2.PersonalData.LastName);
		result.Value.Email.ShouldBe(app2.ContactInfo.Email);
		result.Value.Amount.ShouldBe(app2.OfferConditions.Amount);
		result.Value.DocumentId.ShouldBeNull();
	}

	[Fact]
	public async Task Handle_ShouldReturnFailureOrNullValue_WhenApplicationDoesNotExist()
	{
		// Arrange
		var existingApp = CreateTestApplication(userId: 5);
		await _dbContext.Set<ApplicationEntity>().AddAsync(existingApp);
		await _dbContext.SaveChangesAsync();

		var nonExistingId = 99999;
		var query = new GetApplicationByIdQuery(nonExistingId);

		var result = await _handler.Handle(query, CancellationToken.None);

		result.IsSuccess.ShouldBeFalse();    
		result.Value.ShouldBeNull();
	}

	[Fact]
	public async Task Handle_ShouldReturnFailureOrNull_WhenDatabaseIsEmpty()
	{
		var query = new GetApplicationByIdQuery(1);

		var result = await _handler.Handle(query, CancellationToken.None);

		result.IsSuccess.ShouldBeFalse();        
		result.Value.ShouldBeNull();
	}

	private ApplicationEntity CreateTestApplication(int? userId = null)
	{
		var personal = new ApplicantPersonalInfo("Anna", "Nowak", 29);
		var contact = new ApplicantContactInfo(
			"anna.nowak@example.pl",
			"+48 600 700 800",
			"ul. Marszałkowska 10, 00-950 Warszawa"
		);
		var financials = new ApplicantFinancialInfo(
			Income: 5800m,
			Costs: 2600m,
			Dependents: 1,
			Job: "Accountant"
		);
		var conditions = new OfferConditions(
			Amount: 22000m,
			Duration: 36,
			InterestRate: 7.49m
		);

		return new ApplicationEntity(
			200 + (userId ?? 0),
			userId,
			financials,
			contact,
			personal,
			conditions
		);
	}
}
