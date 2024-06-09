using Moq;
using Xunit;

public class MaximumSurfacePressuresShould
{
    [Fact]
    public void Run()
    {
        // Given
        const byte COMPARTMENT_COUNT = 16;
        float[] aValues = new float[COMPARTMENT_COUNT] { 1.2599f, 1.0000f, 0.8618f, 0.7562f, 0.6667f, 0.5600f, 0.4947f, 0.4500f, 0.4187f, 0.3798f, 0.3497f, 0.3223f, 0.2850f, 0.2737f, 0.2523f, 0.2327f };
        float[] bValues = new float[COMPARTMENT_COUNT] { 0.5050f, 0.6514f, 0.7222f, 0.7825f, 0.8126f, 0.8434f, 0.8693f, 0.8910f, 0.9092f, 0.9222f, 0.9319f, 0.9403f, 0.9477f, 0.9544f, 0.9602f, 0.9653f };
        float[] expectedMaxSurfacePressures = new float[COMPARTMENT_COUNT] { 3.2401f, 2.5352f, 2.2465f, 2.0342f, 1.8973f, 1.7457f, 1.6451f, 1.5723f, 1.5186f, 1.4642f, 1.4228f, 1.3858f, 1.3402f, 1.3215f, 1.2937f, 1.2686f };
        Mock<IDiveStageValidator> diveStageValidator = new();
        IDiveStage diveStage = new DiveStage(diveStageValidator.Object)
        {
            DiveModel = new Zhl16Buhlmann()
            {
                DiveModelProfile = new DiveModelProfile(COMPARTMENT_COUNT)
                {
                    AValues = aValues,
                    BValues = bValues,
                }
            }
        };
        IDiveProfileStage diveProfileStage = new MaximumSurfacePressures(diveStage);

        // When
        diveProfileStage.Run();

        // Then
        for (int i = 0; i < COMPARTMENT_COUNT; i++)
        {
            Assert.Equal(expectedMaxSurfacePressures[i], diveStage.DiveModel.DiveModelProfile.MaxSurfacePressures[i], 4);
        }
    }
}