namespace LoanHub.Backend.Core.EntityAggregates.OfferAggregate.Dto;

public sealed record OfferConditionsDto(decimal amount, uint duration, decimal interestRate);