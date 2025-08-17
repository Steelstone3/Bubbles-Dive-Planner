using Xunit;

public class CentralNervousSystemToxicityShould
{
    [Fact]
    public void Construct()
    {
        // Given
        CentralNervousSystemToxicity centralNervousSystemToxicity = new();

        // Then
        Assert.NotNull(centralNervousSystemToxicity.OxygenPartialPressureConstant);
        Assert.NotEmpty(centralNervousSystemToxicity.OxygenPartialPressureConstant);
        Assert.NotNull(centralNervousSystemToxicity.MaximumSingleDiveDuration);
        Assert.NotEmpty(centralNervousSystemToxicity.MaximumSingleDiveDuration);
        Assert.NotNull(centralNervousSystemToxicity.TotalDailyDuration);
        Assert.NotEmpty(centralNervousSystemToxicity.TotalDailyDuration);
    }
}