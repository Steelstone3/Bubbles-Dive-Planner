using Moq;
using Xunit;

public class TissuePressuresShould
{
    [Fact]
    public void RunTissuePressureStages()
    {
        // Given
        const byte COMPARTMENT_COUNT = 16;
        float[] expectedNitrogenTissuePressures = new float[COMPARTMENT_COUNT] { 4.0417f, 3.0792f, 2.4713f, 2.0243f, 1.6843f, 1.4439f, 1.2634f, 1.13f, 1.0334f, 0.9731f, 0.9337f, 0.9029f, 0.8788f, 0.8596f, 0.8446f, 0.8329f };
        float[] expectedHeliumTissuePressures = new float[COMPARTMENT_COUNT] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
        float[] expectedTotalTissuePressures = new float[COMPARTMENT_COUNT] { 4.0417f, 3.0792f, 2.4713f, 2.0243f, 1.6843f, 1.4439f, 1.2634f, 1.13f, 1.0334f, 0.9731f, 0.9337f, 0.9029f, 0.8788f, 0.8596f, 0.8446f, 0.8329f };
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
            DiveModel = new Zhl16Buhlmann()
            {
                DiveModelProfile = new DiveModelProfile(COMPARTMENT_COUNT)
                {
                    NitrogenAtPressure = 4.74f,
                    OxygenAtPressure = 1.26f,
                }
            },
            DiveStep = diveStep.Object,
            Cylinder = cylinder.Object
        };
        IDiveProfileStage diveProfileStage = new TissuePressures(diveStage);

        // When
        diveProfileStage.Run();

        // Then
        for (int i = 0; i < COMPARTMENT_COUNT; i++)
        {
            Assert.Equal(expectedNitrogenTissuePressures[i], diveStage.DiveModel.DiveModelProfile.NitrogenTissuePressures[i], 4);
            Assert.Equal(expectedHeliumTissuePressures[i], diveStage.DiveModel.DiveModelProfile.HeliumTissuePressures[i], 4);
            Assert.Equal(expectedTotalTissuePressures[i], diveStage.DiveModel.DiveModelProfile.TotalTissuePressures[i], 4);
        }
    }
}