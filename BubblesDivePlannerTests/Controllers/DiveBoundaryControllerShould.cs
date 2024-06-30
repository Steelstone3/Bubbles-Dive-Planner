using Moq;
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
        List<IDiveStage> diveStages = null;

        // When
        float diveCeiling = diveBoundaryController.GetOverallDiveCeiling(diveStages);

        // Then
        Assert.Equal(expectedDiveCeiling, diveCeiling);
    }

    [Fact]
    public void GetOverallDiveCeiling()
    {
        // Given
        float expectedDiveCeiling = 3.0F;
        Mock<IDiveModelProfile> diveModelProfile =new();
        diveModelProfile.Setup(dp => dp.DiveCeiling).Returns(expectedDiveCeiling);
        Mock<IDiveModel> diveModel = new();
        diveModel.Setup(dm => dm.DiveModelProfile).Returns(diveModelProfile.Object);
        Mock<IDiveStage> diveStage = new();
        diveStage.Setup(ds => ds.DiveModel).Returns(diveModel.Object);
        List<IDiveStage> diveStages = new() { diveStage.Object };

        // When
        float diveCeiling = diveBoundaryController.GetOverallDiveCeiling(diveStages);

        // Then
        Assert.Equal(expectedDiveCeiling, diveCeiling);
        diveStage.VerifyAll();
    }
}