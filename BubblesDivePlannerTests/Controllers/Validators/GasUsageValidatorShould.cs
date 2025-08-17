using Moq;
using Xunit;

public class GasUsageValidatorShould
{
    IValidator<GasUsage> gasUsageValidator = new GasUsageValidator();

    [Theory]
    [InlineData(6)]
    [InlineData(30)]
    [InlineData(10)]
    [InlineData(12)]
    public void ValidateValidGasUsage(byte surfaceAirConsumptionRate)
    {
        // Given
        GasUsage gasUsage = new()
        {
            SurfaceAirConsumptionRate = surfaceAirConsumptionRate
        };

        // When
        var isValid = gasUsageValidator.Validate(gasUsage);

        // Then
        Assert.True(isValid);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(31)]
    public void ValidateInvalidGasUsage(byte surfaceAirConsumptionRate)
    {
        // Given
        GasUsage gasUsage = new()
        {
            SurfaceAirConsumptionRate = surfaceAirConsumptionRate
        };

        // When
        var isValid = gasUsageValidator.Validate(gasUsage);

        // Then
        Assert.False(isValid);
    }
}