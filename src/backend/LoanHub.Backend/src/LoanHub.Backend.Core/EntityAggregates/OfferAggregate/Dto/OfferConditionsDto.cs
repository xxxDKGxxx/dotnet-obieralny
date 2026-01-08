namespace LoanHub.Backend.Core.EntityAggregates.OfferAggregate.Dto;

public sealed record OfferConditionsDto(decimal Amount, uint Duration, decimal InterestRate);