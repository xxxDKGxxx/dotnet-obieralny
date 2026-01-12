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
	   decimal requestedAmount,
	   uint requestedDuration,
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

		var interpolatedAmount = Lerp(
			offer.AmountRange.Min,
			offer.AmountRange.Max,
			clientScore);

		var targetAmount = Math.Min(interpolatedAmount, requestedAmount);
		targetAmount = Math.Clamp(targetAmount, offer.AmountRange.Min, offer.AmountRange.Max);

		var interpolatedDuration = (uint)Lerp(
			offer.DurationRange.Min,
			offer.DurationRange.Max,
			clientScore);

		var minCapacityDuration = CalculateMinDuration(
			targetAmount,
			interpolatedRate,
			monthlyIncome,
			monthlyCosts,
			dependants);

		var targetDuration = Math.Max(requestedDuration, interpolatedDuration);
		targetDuration = Math.Max(targetDuration, minCapacityDuration);

		var ageMaxDuration = (uint)Math.Max(0, (MaxBorrowerAge - age) * 12);

		var finalDuration = Math.Min(targetDuration, ageMaxDuration);
		finalDuration = Math.Clamp(finalDuration, offer.DurationRange.Min, offer.DurationRange.Max);

		var capacityAmount = AdjustAmountToCapacity(
			targetAmount,
			finalDuration,
			interpolatedRate,
			monthlyIncome,
			monthlyCosts,
			dependants);

		var finalAmount = Math.Min(targetAmount, capacityAmount);
		finalAmount = Math.Clamp(finalAmount, offer.AmountRange.Min, offer.AmountRange.Max);

		return new OfferConditionsDto(
			Math.Round(finalAmount, 2),
			finalDuration,
			Math.Round(interpolatedRate, 2));
	}

	private static uint CalculateMinDuration(
		decimal amount,
		decimal ratePct,
		decimal income,
		decimal costs,
		int dependants)
	{
		var disposableIncome = income - costs - (dependants * CostPerDependant);
		if (disposableIncome <= 0)
		{
			return uint.MaxValue;
		}

		var maxInstallment = (double)(disposableIncome * MaxDebtToIncomeRatio);
		var r = (double)(ratePct / 100 / 12);
		var pv = (double)amount;

		// If rate is 0, pmt = pv / n => n = pv / pmt
		if (ratePct == 0)
		{
			if (maxInstallment <= 0)
			{
				return uint.MaxValue;
			}

			return (uint)Math.Ceiling(pv / maxInstallment);
		}

		// pv = pmt * (1 - (1+r)^-n) / r
		// pv * r / pmt = 1 - (1+r)^-n
		// (1+r)^-n = 1 - (pv * r / pmt)

		var ratio = pv * r / maxInstallment;
		if (ratio >= 1.0)
		{
			// Cannot afford even interest
			return uint.MaxValue;
		}

		// -n * ln(1+r) = ln(1 - ratio)
		// n = - ln(1 - ratio) / ln(1+r)

		var n = -Math.Log(1 - ratio) / Math.Log(1 + r);
		return (uint)Math.Ceiling(n);
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