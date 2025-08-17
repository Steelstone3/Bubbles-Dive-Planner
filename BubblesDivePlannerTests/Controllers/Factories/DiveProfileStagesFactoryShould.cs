using Moq;
using Xunit;

public class DiveProfileStagesFactoryShould
{
    private IDiveProfileStagesFactory diveProfileStagesCommand = new DiveProfileStagesFactory();

    [Fact]
    public void Create()
    {
        // Given
        Mock<DiveStage> diveStage = new();

        // When
        IDiveProfileStage[] stages = diveProfileStagesCommand.Create(diveStage.Object);

        // Then
        Assert.NotNull(stages);
        Assert.NotEmpty(stages);
        Assert.IsType<AmbientPressures>(stages[0]);
        Assert.IsType<TissuePressures>(stages[1]);
        Assert.IsType<AbValues>(stages[2]);
        Assert.IsType<ToleratedAmbientPressures>(stages[3]);
        Assert.IsType<MaximumSurfacePressures>(stages[4]);
        Assert.IsType<CompartmentLoads>(stages[5]);
    }
}
