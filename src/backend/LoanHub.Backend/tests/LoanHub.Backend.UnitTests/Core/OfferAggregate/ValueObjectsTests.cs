namespace LoanHub.Backend.UnitTests.Core.OfferAggregate;

public class AmountRangeTests
{
	[Fact]
	public void Constructor_WhenValuesAreValid_ShouldCreateInstance()
	{
		var min = 1000m;
		var max = 5000m;

		var range = new AmountRange(min, max);

		range.Min.ShouldBe(min);
		range.Max.ShouldBe(max);
	}

	[Fact]
	public void Constructor_WhenMinGreaterThanMax_ShouldThrowArgumentException()
	{
		var exception = Should.Throw<ArgumentException>(() =>
		{
			return new AmountRange(2000m, 1000m);
		});
		exception.Message.ShouldContain("Min value must be less than max value");
	}

	[Fact]
	public void Constructor_WhenMaxIsZeroOrNegative_ShouldThrowArgumentException()
	{
		Should.Throw<ArgumentException>(() =>
		{
			return new AmountRange(10, -5);
		});
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-100)]
	public void Constructor_WhenMinIsZeroOrNegative_ShouldThrowArgumentException(decimal invalidMin)
	{
		var exception = Should.Throw<ArgumentException>(() =>
		{
			return new AmountRange(invalidMin, 1000m);
		});
		exception.Message.ShouldContain("Min value must be greater than 0");
	}

	[Theory]
	[InlineData(1000, 2000, 1500, true)]
	[InlineData(1000, 2000, 1000, true)]
	[InlineData(1000, 2000, 2000, true)]
	[InlineData(1000, 2000, 999, false)]
	[InlineData(1000, 2000, 2001, false)]
	public void IsInRange_ShouldReturnCorrectResult(decimal min, decimal max, decimal valueToCheck, bool expected)
	{
		var range = new AmountRange(min, max);

		var result = range.IsInRange(valueToCheck);

		result.ShouldBe(expected);
	}

	[Fact]
	public void Equality_WhenValuesAreSame_ShouldBeEqual()
	{
		var range1 = new AmountRange(100, 200);
		var range2 = new AmountRange(100, 200);

		range1.ShouldBe(range2);
		range1.Equals(range2).ShouldBeTrue();
	}
}

public class DurationRangeTests
{
	[Fact]
	public void Constructor_WhenMinGreaterThanMax_ShouldThrowException()
	{
		Should.Throw<ArgumentException>(() =>
		{
			return new DurationRange(12, 6);
		});
	}

	[Fact]
	public void IsInRange_ShouldWorkForUInt()
	{
		var range = new DurationRange(6, 24);
		range.IsInRange(12).ShouldBeTrue();
		range.IsInRange(3).ShouldBeFalse();
	}
}

public class ValidRangeTests
{
	[Fact]
	public void Constructor_WhenMinDateAfterMaxDate_ShouldThrowException()
	{
		var now = DateTime.UtcNow;
		Should.Throw<ArgumentException>(() =>
		{
			return new ValidRange(now.AddDays(1), now);
		});
	}

	[Fact]
	public void IsInRange_ShouldWorkForDates()
	{
		var start = DateTime.UtcNow;
		var end = start.AddDays(10);
		var range = new ValidRange(start, end);

		range.IsInRange(start.AddDays(5)).ShouldBeTrue();
		range.IsInRange(start.AddDays(11)).ShouldBeFalse();
	}
}