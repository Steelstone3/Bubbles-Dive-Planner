using Moq;
using Xunit;

public class CylinderControllerShould
{
    private readonly ICylinderController cylinderController = new CylinderController();

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(12, 0, 0)]
    [InlineData(0, 200, 0)]
    [InlineData(12, 200, 2400)]
    [InlineData(12, 100, 1200)]
    [InlineData(6, 200, 1200)]
    public void CalculateInitialPressurisedVolume(byte volume, ushort pressure, ushort expectedInitialPressurisedVolume)
    {
        // When
        var initialPressurisedVolume = cylinderController.CalculateInitialPressurisedVolume(volume, pressure);

        // Then
        Assert.Equal(expectedInitialPressurisedVolume, initialPressurisedVolume);
    }

    [Theory]
    [InlineData(21.0F, 0.0F, 79.0F)]
    [InlineData(0.0F, 10.0F, 90.0F)]
    [InlineData(21.0F, 10.0F, 69.0F)]
    public void CalculateNitrogen(float oxygen, float helium, float expectedNitrogen)
    {
        // When
        float nitrogen = cylinderController.CalculateNitrogen(oxygen, helium);

        // Then
        Assert.Equal(expectedNitrogen, nitrogen);
    }

    [Fact]
    public void UpdateGasUsage()
    {
        // Given
        Mock<IDiveStepValidator> diveStepValidator = new();
        DiveStep diveStep = new(diveStepValidator.Object)
        {
            Depth = 50,
            Time = 10,
        };
        Mock<IGasUsageValidator> gasUsageValidator = new();
        GasUsage gasUsage = new(gasUsageValidator.Object)
        {
            Remaining = 2400,
            SurfaceAirConsumptionRate = 12,
        };

        // When
        IGasUsage updatedGasUsage = cylinderController.UpdateGasUsage(diveStep, gasUsage);

        // Then
        Assert.Equal(1680, updatedGasUsage.Remaining);
        Assert.Equal(720, updatedGasUsage.Used);
    }
}