using LoanHub.Backend.Core.EntityAggregates.OfferAggregate;
using LoanHub.Backend.Core.EntityAggregates.OfferAggregate.Dto;
using LoanHub.Backend.Core.Interfaces;

namespace LoanHub.Backend.Core.Services;

public sealed class OfferCalculatorService : IOfferCalculator
{
	private const int MaxBorrowerAge = 75;
	private const decimal CostPerDependant = 500m;
	private const decimal MaxDebtToIncomeRatio = 0.50m;

	public OfferConditionsDto Calculate(
	   Offer offer,
	   decimal monthlyIncome,
	   decimal monthlyCosts,
	   int age,
	   int dependants)
	{
		if (!IsValidRequest(offer, monthlyIncome, monthlyCosts, age))
		{
			return GetWorstCaseConditions(offer);
		}

		var clientScore = CalculateClientScore(monthlyIncome, monthlyCosts, age, dependants);

		var interpolatedRate = Lerp(
			offer.InterestRateRange.Max,
			offer.InterestRateRange.Min,
			clientScore);

		var ageMaxDuration = (uint)Math.Max(0, (MaxBorrowerAge - age) * 12);

		var interpolatedDuration = (uint)Lerp(
			offer.DurationRange.Min,
			offer.DurationRange.Max,
			clientScore);

		var possibleDuration = Math.Min(interpolatedDuration, ageMaxDuration);

		var finalDuration = Math.Max(possibleDuration, offer.DurationRange.Min);

		var interpolatedAmount = Lerp(
			offer.AmountRange.Min,
			offer.AmountRange.Max,
			clientScore);

		var capacityAmount = AdjustAmountToCapacity(
			interpolatedAmount,
			finalDuration,
			interpolatedRate,
			monthlyIncome,
			monthlyCosts,
			dependants);

		var finalAmount = Math.Max(capacityAmount, offer.AmountRange.Min);

		finalAmount = Math.Min(finalAmount, offer.AmountRange.Max);

		return new OfferConditionsDto(
			Math.Round(finalAmount, 2),
			finalDuration,
			Math.Round(interpolatedRate, 2));
	}

	private static OfferConditionsDto GetWorstCaseConditions(Offer offer)
	{
		return new OfferConditionsDto(
			offer.AmountRange.Min,
			offer.DurationRange.Min,
			offer.InterestRateRange.Max
		);
	}

	private static bool IsValidRequest(Offer offer, decimal income, decimal costs, int age)
	{
		var now = DateTime.UtcNow;

		if (now < offer.ValidRange.Min || now > offer.ValidRange.Max)
		{
			return false;
		}

		if (income <= 0)
		{
			return false;
		}

		if (age > MaxBorrowerAge)
		{
			return false;
		}

		if (income <= costs)
		{
			return false;
		}

		return true;
	}

	private static decimal Lerp(decimal start, decimal end, double t)
	{
		return start + ((end - start) * (decimal)t);
	}

	private static double Lerp(uint start, uint end, double t)
	{
		return start + ((end - start) * t);
	}

	private static double CalculateClientScore(decimal income, decimal costs, int age, int dependants)
	{
		if (income == 0)
		{
			return 0.0;
		}

		var realCosts = costs + (dependants * CostPerDependant);
		var financialRatio = (double)((income - realCosts) / income);

		var financialScore = Math.Clamp((financialRatio - 0.05) / (0.6 - 0.05), 0.0, 1.0);
		var ageScore = 1.0 - Math.Clamp((age - 18.0) / (MaxBorrowerAge - 18.0), 0.0, 1.0);

		var finalScore = (financialScore * 0.7) + (ageScore * 0.3);

		return Math.Clamp(finalScore, 0.0, 1.0);
	}

	private static decimal AdjustAmountToCapacity(
	   decimal desiredAmount,
	   uint duration,
	   decimal ratePct,
	   decimal income,
	   decimal costs,
	   int dependants)
	{
		var disposableIncome = income - costs - (dependants * CostPerDependant);

		if (disposableIncome <= 0)
		{
			return 0m;
		}

		var maxInstallment = disposableIncome * MaxDebtToIncomeRatio;

		var r = (double)(ratePct / 100 / 12);
		var n = (double)duration;
		var pmt = (double)maxInstallment;

		var maxMathAmount = ratePct == 0
			? maxInstallment * duration
			: (decimal)(pmt * (1 - Math.Pow(1 + r, -n)) / r);

		return Math.Max(0, Math.Min(desiredAmount, maxMathAmount));
	}
}