using System.Runtime.InteropServices;
using Moq;
using Xunit;

public class GasUsageValidatorShould
{
    IGasUsageValidator gasUsageValidator = new GasUsageValidator();

    [Theory]
    [InlineData(6)]
    [InlineData(30)]
    [InlineData(10)]
    [InlineData(12)]
    public void ValidateValidGasUsage(byte surfaceAirConsumptionRate)
    {
        // Given
        Mock<IGasUsage> gasUsage = new();
        gasUsage.Setup(gu => gu.SurfaceAirConsumptionRate).Returns(surfaceAirConsumptionRate);

        // When
        var isValid = gasUsageValidator.Validate(gasUsage.Object);

        // Then
        Assert.True(isValid);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(31)]
    public void ValidateInvalidGasUsage(byte surfaceAirConsumptionRate)
    {
        // Given
        Mock<IGasUsage> gasUsage = new();
        gasUsage.Setup(gu => gu.SurfaceAirConsumptionRate).Returns(surfaceAirConsumptionRate);

        // When
        var isValid = gasUsageValidator.Validate(gasUsage.Object);

        // Then
        Assert.False(isValid);
    }
}