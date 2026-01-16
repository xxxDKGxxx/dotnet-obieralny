using System.Runtime.InteropServices.ComTypes;
using Ardalis.GuardClauses;

namespace LoanHub.Backend.UseCases.Features.Application.Create;

public sealed class CreateApplicationCommandHandler(
	IRepository<ApplicationEntity> applicationsRepository,
	IReadRepository<OfferEntity> offersRepository,
	IReadRepository<UserEntity> usersRepository,
	IOfferCalculator offerCalculator,
	IMapper mapper,
	INotificationService notificationService) :
	ICommandHandler<CreateApplicationCommand, Result<ApplicationDto>>
{
	public async Task<Result<ApplicationDto>> Handle(
		CreateApplicationCommand request,
		CancellationToken cancellationToken)
	{
		var validationResult = Validate(request);

		if (validationResult is not null)
		{
			return validationResult;
		}

		if (request.UserId != request.RequestingUserId)
		{
			return Result.Forbidden("You do not have permission to create an application for this user");
		}

		var offer = await offersRepository.GetByIdAsync(request.OfferId, cancellationToken);

		if (offer is null)
		{
			return Result.NotFound($"Offer with id {request.OfferId} not found");
		}

		if (request.UserId is not null)
		{
			var user = await usersRepository.GetByIdAsync(request.UserId.Value, cancellationToken);

			if (user is null)
			{
				return Result.NotFound($"User with id {request.UserId} not found");
			}
		}

		var offerConditionsDto = offerCalculator.Calculate(
			offer,
			request.Amount,
			request.Duration,
			request.Financials.Income,
			request.Financials.Costs,
			request.PersonalData.Age,
			request.Financials.Dependents);

		var offerConditions = mapper.Map<OfferConditions>(offerConditionsDto);

		var newApplication = new ApplicationEntity(
			request.OfferId,
			request.UserId,
			request.Financials,
			request.Contact,
			request.PersonalData,
			offerConditions);

		newApplication = await applicationsRepository.AddAsync(newApplication, cancellationToken);

		notificationService.NotifyApplicationCreated(
			newApplication.ContactInfo.Email,
			offer.Title,
			newApplication.PersonalData.FirstName);

		return Result.Success(mapper.Map<ApplicationDto>(newApplication));
	}

	private static Result? Validate(CreateApplicationCommand request)
	{
		try
		{
			Guard.Against.Negative(request.OfferId);
			Guard.Against.Negative(request.Financials.Income - request.Financials.Costs);
			Guard.Against.NegativeOrZero(request.Amount);
			Guard.Against.NegativeOrZero(request.Duration);
			Guard.Against.NegativeOrZero(request.Financials.Income);
			Guard.Against.NegativeOrZero(request.Financials.Costs);
			Guard.Against.NegativeOrZero(request.PersonalData.Age);
			Guard.Against.NegativeOrZero(request.Financials.Dependents);
			Guard.Against.Empty(request.Financials.Job, nameof(request.Financials.Job));
			Guard.Against.Empty(request.PersonalData.FirstName, nameof(request.PersonalData.FirstName));
			Guard.Against.Empty(request.PersonalData.LastName, nameof(request.PersonalData.LastName));
		}
		catch (Exception e)
		{
			return Result.Invalid(new ValidationError(e.Message));
		}

		return null;
	}
}