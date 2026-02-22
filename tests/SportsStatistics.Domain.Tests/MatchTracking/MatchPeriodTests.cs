using SportsStatistics.Domain.MatchTracking;
using Shouldly;
using Xunit;

namespace SportsStatistics.Domain.Tests.MatchTracking;

public class MatchPeriodTests
{
    #region Enumeration Values Tests

    [Fact]
    public void MatchPeriod_ShouldHaveCorrectFirstHalfValue()
    {
        // Arrange.
        // Act.
        var period = MatchPeriod.FirstHalf;

        // Assert.
        period.Value.ShouldBe(0);
        period.Name.ShouldBe("FirstHalf");
    }

    [Fact]
    public void MatchPeriod_ShouldHaveCorrectFirstHalfStoppageValue()
    {
        // Arrange.
        // Act.
        var period = MatchPeriod.FirstHalfStoppage;

        // Assert.
        period.Value.ShouldBe(1);
        period.Name.ShouldBe("FirstHalfStoppage");
    }

    [Fact]
    public void MatchPeriod_ShouldHaveCorrectSecondHalfValue()
    {
        // Arrange.
        // Act.
        var period = MatchPeriod.SecondHalf;

        // Assert.
        period.Value.ShouldBe(3);
        period.Name.ShouldBe("SecondHalf");
    }

    [Fact]
    public void MatchPeriod_ShouldHaveCorrectSecondHalfStoppageValue()
    {
        // Arrange.
        // Act.
        var period = MatchPeriod.SecondHalfStoppage;

        // Assert.
        period.Value.ShouldBe(4);
        period.Name.ShouldBe("SecondHalfStoppage");
    }

    #endregion

    #region IsStoppageTime Tests

    public static TheoryData<MatchPeriod, bool> IsStoppageTimeTestData => new()
    {
        { MatchPeriod.FirstHalf, false },
        { MatchPeriod.FirstHalfStoppage, true },
        { MatchPeriod.HalfTime, false },
        { MatchPeriod.SecondHalf, false },
        { MatchPeriod.SecondHalfStoppage, true },
        { MatchPeriod.FullTime, false },
    };

    [Theory]
    [MemberData(nameof(IsStoppageTimeTestData))]
    public void IsStoppageTime_ShouldReturnCorrectValue(MatchPeriod period, bool expected)
    {
        // Arrange.
        // Act.
        var result = period.IsStoppageTime();

        // Assert.
        result.ShouldBe(expected);
    }

    #endregion

    #region IsPlayingPeriod Tests

    public static TheoryData<MatchPeriod, bool> IsPlayingPeriodTestData => new()
    {
        { MatchPeriod.FirstHalf, true },
        { MatchPeriod.FirstHalfStoppage, true },
        { MatchPeriod.HalfTime, false },
        { MatchPeriod.SecondHalf, true },
        { MatchPeriod.SecondHalfStoppage, true },
        { MatchPeriod.FullTime, false },
    };

    [Theory]
    [MemberData(nameof(IsPlayingPeriodTestData))]
    public void IsPlayingPeriod_ShouldReturnCorrectValue(MatchPeriod period, bool expected)
    {
        // Arrange.
        // Act.
        var result = period.IsPlayingPeriod();

        // Assert.
        result.ShouldBe(expected);
    }

    #endregion

    #region IsExtraTimePeriod Tests

    public static TheoryData<MatchPeriod, bool> IsExtraTimePeriodTestData => new()
    {
        { MatchPeriod.FirstHalf, false },
        { MatchPeriod.FirstHalfStoppage, false },
        { MatchPeriod.SecondHalf, false },
        { MatchPeriod.SecondHalfStoppage, false },
    };

    #endregion

    #region GetStoppageBaseMinute Tests

    public static TheoryData<MatchPeriod, int?> GetStoppageBaseMinuteTestData => new()
    {
        { MatchPeriod.FirstHalf, null },
        { MatchPeriod.FirstHalfStoppage, 45 },
        { MatchPeriod.SecondHalf, null },
        { MatchPeriod.SecondHalfStoppage, 90 },
    };

    [Theory]
    [MemberData(nameof(GetStoppageBaseMinuteTestData))]
    public void GetStoppageBaseMinute_ShouldReturnCorrectValue(MatchPeriod period, int? expected)
    {
        // Arrange.
        // Act.
        var result = period.GetStoppageBaseMinute();

        // Assert.
        result.ShouldBe(expected);
    }

    #endregion

    #region List Tests

    [Fact]
    public void List_ShouldContainAllPeriods()
    {
        // Arrange.
        // Act.
        var periods = MatchPeriod.List;

        // Assert.
        periods.Count.ShouldBe(12);
        periods.ShouldContain(MatchPeriod.FirstHalf);
        periods.ShouldContain(MatchPeriod.FirstHalfStoppage);
        periods.ShouldContain(MatchPeriod.HalfTime);
        periods.ShouldContain(MatchPeriod.SecondHalf);
        periods.ShouldContain(MatchPeriod.SecondHalfStoppage);
        periods.ShouldContain(MatchPeriod.FullTime);
    }

    #endregion
}
