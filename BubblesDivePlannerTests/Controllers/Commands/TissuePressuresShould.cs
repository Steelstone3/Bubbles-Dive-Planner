using Moq;
using Xunit;

public class TissuePressuresShould
{
    [Fact]
    public void RunTissuePressureStages()
    {
        // Given
        DiveModelFactory diveModelFactory = new();
        Mock<IDiveBoundaryController> diveBoundaryController = new();
        const byte COMPARTMENT_COUNT = 16;
        float[] expectedNitrogenTissuePressures = new float[COMPARTMENT_COUNT] { 4.0417f, 3.0792f, 2.4713f, 2.0243f, 1.6843f, 1.4439f, 1.2634f, 1.13f, 1.0334f, 0.9731f, 0.9337f, 0.9029f, 0.8788f, 0.8596f, 0.8446f, 0.8329f };
        float[] expectedHeliumTissuePressures = new float[COMPARTMENT_COUNT] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
        float[] expectedTotalTissuePressures = new float[COMPARTMENT_COUNT] { 4.0417f, 3.0792f, 2.4713f, 2.0243f, 1.6843f, 1.4439f, 1.2634f, 1.13f, 1.0334f, 0.9731f, 0.9337f, 0.9029f, 0.8788f, 0.8596f, 0.8446f, 0.8329f };
        DiveStep diveStep = new()
        {
            Depth = 50,
            Time = 10,
        };
        GasMixture gasMixture = new()
        {
            Oxygen = 21,
            Helium = 0
        };
        Cylinder cylinder = new()
        {
            GasMixture = gasMixture
        };
        Mock<DiveStageValidator> diveStageValidator = new();

        DiveModel diveModel = diveModelFactory.CreateZhl16Buhlmann();
        diveModel.DiveModelProfile = new DiveModelProfile(COMPARTMENT_COUNT)
        {
            NitrogenAtPressure = 4.74f,
            OxygenAtPressure = 1.26f,
        };

        DiveStage diveStage = new DiveStage()
        {
            DiveModel = diveModel,
            DiveStep = diveStep,
            Cylinder = cylinder
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