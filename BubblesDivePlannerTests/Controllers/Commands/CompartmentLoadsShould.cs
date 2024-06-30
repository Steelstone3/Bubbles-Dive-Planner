using Moq;
using Xunit;

public class CompartmentLoadsShould
{
    [Fact]
    public void Run()
    {
        // Given
        Mock<IDiveBoundaryController> diveBoundaryController = new();
        const byte COMPARTMENT_COUNT = 16;
        float[] totalTissuePressures = new float[COMPARTMENT_COUNT] { 4.0417f, 3.0792f, 2.4713f, 2.0243f, 1.6843f, 1.4439f, 1.2634f, 1.13f, 1.0334f, 0.9731f, 0.9337f, 0.9029f, 0.8788f, 0.8596f, 0.8446f, 0.8329f };
        float[] maxSurfacePressures = new float[COMPARTMENT_COUNT] { 3.2401f, 2.5352f, 2.2465f, 2.0342f, 1.8973f, 1.7457f, 1.6451f, 1.5723f, 1.5186f, 1.4642f, 1.4228f, 1.3858f, 1.3402f, 1.3215f, 1.2937f, 1.2686f };
        float[] expectedCompartmentLoads = new float[COMPARTMENT_COUNT] { 124.74f, 121.46f, 110.01f, 99.51f, 88.77f, 82.71f, 76.8f, 71.87f, 68.05f, 66.46f, 65.62f, 65.15f, 65.57f, 65.05f, 65.29f, 65.66f };
        Mock<IDiveStageValidator> diveStageValidator = new();
        IDiveStage diveStage = new DiveStage(diveStageValidator.Object)
        {
            DiveModel = new Zhl16Buhlmann()
            {
                DiveModelProfile = new DiveModelProfile(COMPARTMENT_COUNT, diveBoundaryController.Object)
                {
                    TotalTissuePressures = totalTissuePressures,
                    MaxSurfacePressures = maxSurfacePressures,
                }
            },
        };
        IDiveProfileStage diveProfileStage = new CompartmentLoads(diveStage);

        // When
        diveProfileStage.Run();

        // Then
        for (int i = 0; i < COMPARTMENT_COUNT; i++)
        {
            Assert.Equal(expectedCompartmentLoads[i], diveStage.DiveModel.DiveModelProfile.CompartmentLoads[i], 2);
        }
    }
}
