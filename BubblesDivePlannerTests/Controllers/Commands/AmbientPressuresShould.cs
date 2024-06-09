using Moq;
using Xunit;

public class AmbientPressuresShould
{
    [Fact]
    public void Run()
    {
        // Given
        float expectedOxygenAtPressure = 1.26f;
        float expectedNitrogenAtPressure = 4.74f;
        float expectedHeliumAtPressure = 0.0f;
        Mock<IDiveStep> diveStep = new();
        diveStep.Setup(ds => ds.Depth).Returns(50);
        diveStep.Setup(ds => ds.Time).Returns(10);
        Mock<IGasMixture> gasMixture = new();
        gasMixture.Setup(gm => gm.Oxygen).Returns(21);
        gasMixture.Setup(gm => gm.Helium).Returns(0);
        gasMixture.Setup(gm => gm.Nitrogen).Returns(79);
        Mock<ICylinder> cylinder = new();
        cylinder.Setup(c => c.GasMixture).Returns(gasMixture.Object);
        Mock<IDiveStageValidator> diveStageValidator = new();
        IDiveStage diveStage = new DiveStage(diveStageValidator.Object)
        {
            DiveModel = new Zhl16Buhlmann(),
            DiveStep = diveStep.Object,
            Cylinder = cylinder.Object,
        };
        IDiveProfileStage diveProfileStage = new AmbientPressures(diveStage);

        // When
        diveProfileStage.Run();

        // Then
        Assert.Equal(expectedOxygenAtPressure, diveStage.DiveModel.DiveModelProfile.OxygenAtPressure, 4);
        Assert.Equal(expectedHeliumAtPressure, diveStage.DiveModel.DiveModelProfile.HeliumAtPressure, 4);
        Assert.Equal(expectedNitrogenAtPressure, diveStage.DiveModel.DiveModelProfile.NitrogenAtPressure, 4);
    }
}