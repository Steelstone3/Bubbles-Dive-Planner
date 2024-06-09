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

    [Fact(Skip = "To do")]
    public void CalculateRemainingPressurisedVolume()
    {
        // Given
    
        // When
    
        // Then
    }

    [Fact(Skip = "To do")]
    public void CalculateGasUsed()
    {
        // Given
    
        // When
    
        // Then
    }
}