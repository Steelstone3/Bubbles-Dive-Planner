using Xunit;

public class DiveBoundaryControllerShould
{
    private readonly IDiveBoundaryController diveBoundaryController = new DiveBoundaryController();

    [Theory]
    [InlineData(21.0F, 56.67F)]
    [InlineData(32.0F, 33.75F)]
    [InlineData(36.0F, 28.89F)]
    [InlineData(40.0F, 25.0F)]
    [InlineData(50.0F, 18.0F)]
    [InlineData(100.0F, 4.0F)]
    public void CalculateMaximumOperatingDepth(float oxygen, float expectedMaximumOperatingDepth)
    {
        // When
        float maximumOperatingDepth = diveBoundaryController.CalculateMaximumOperatingDepth(oxygen);

        // Then
        Assert.Equal(expectedMaximumOperatingDepth, maximumOperatingDepth, 5);
    }
    
    [Fact]
    public void CalculateDiveCeiling()
    {
        // Given
    
        // When
    
        // Then
    }
}