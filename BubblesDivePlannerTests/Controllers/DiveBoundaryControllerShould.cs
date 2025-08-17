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

    [Theory]
    [InlineData(new float[] { 0.4F, 1.0F, 1.2F }, 2.0F)]
    [InlineData(new float[] { 0.4F, 0.2F, 0.1F }, -6.0F)]
    [InlineData(new float[] { 3.0F, 2.0F, 1.0F }, 20.0F)]
    public void CalculateDiveCeiling(float[] toleratedAmbientPressures, float expectedDiveCeiling)
    {
        // When
        float diveCeiling = diveBoundaryController.CalculateDiveCeiling(toleratedAmbientPressures);

        // Then
        Assert.Equal(expectedDiveCeiling, diveCeiling);
    }

    [Fact]
    public void GetOverallDiveCeilingWithNoResults()
    {
        // Given
        float expectedDiveCeiling = 0.0F;
        List<DiveStage> diveStages = null;

        // When
        float diveCeiling = diveBoundaryController.GetOverallDiveCeiling(diveStages);

        // Then
        Assert.Equal(expectedDiveCeiling, diveCeiling);
    }

    [Fact]
    public void GetOverallDiveCeiling()
    {
        // Given
        float expectedDiveCeiling = 55.0F;
        DiveModelProfile diveModelProfile = new(3)
        {
            ToleratedAmbientPressures = [2.0F, 4.0F, 6.5F],
        };
        DiveModel diveModel = new()
        {
            DiveModelProfile = diveModelProfile
        };
        DiveStage diveStage = new()
        {
            DiveModel = diveModel
        };
        List<DiveStage> diveStages = new() { diveStage };

        // When
        float diveCeiling = diveBoundaryController.GetOverallDiveCeiling(diveStages);

        // Then
        Assert.Equal(expectedDiveCeiling, diveCeiling);
    }
}