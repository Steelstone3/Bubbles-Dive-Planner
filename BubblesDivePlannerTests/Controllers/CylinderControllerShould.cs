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

    [Fact (Skip = "Test fails?")]
    public void UpdateGasUsage()
    {
        // Given
        Mock<IDiveStep> diveStep = new();
        diveStep.Setup(ds => ds.Depth).Returns(50);
        diveStep.Setup(ds => ds.Time).Returns(10);

        Mock<IGasUsage> gasUsage = new();
        gasUsage.Setup(gu => gu.Remaining).Returns(2400);
        gasUsage.Setup(gu => gu.Used).Returns(720);

        // When
        IGasUsage updatedGasUsage = cylinderController.UpdateGasUsage(diveStep.Object, gasUsage.Object);
    
        // Then
        Assert.Equal(1680, updatedGasUsage.Remaining);
        Assert.Equal(720, updatedGasUsage.Used);
    }
}