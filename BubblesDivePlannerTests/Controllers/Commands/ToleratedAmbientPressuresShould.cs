using Xunit;

public class ToleratedAmbientPressuresShould
{
    [Fact]
    public void Run()
    {
        // Given
        const byte COMPARTMENT_COUNT = 16;
        float[] totalTissuePressures = new float[COMPARTMENT_COUNT] { 4.0417f, 3.0792f, 2.4713f, 2.0243f, 1.6843f, 1.4439f, 1.2634f, 1.13f, 1.0334f, 0.9731f, 0.9337f, 0.9029f, 0.8788f, 0.8596f, 0.8446f, 0.8329f };
        float[] aValues = new float[COMPARTMENT_COUNT] { 1.2559f, 1.0000f, 0.8618f, 0.7562f, 0.6667f, 0.5600f, 0.4947f, 0.4500f, 0.4187f, 0.3798f, 0.3497f, 0.3223f, 0.2850f, 0.2737f, 0.2523f, 0.2327f };
        float[] bValues = new float[COMPARTMENT_COUNT] { 0.5050f, 0.6514f, 0.7222f, 0.7825f, 0.8126f, 0.8434f, 0.8693f, 0.8910f, 0.9092f, 0.9222f, 0.9319f, 0.9403f, 0.9477f, 0.9544f, 0.9602f, 0.9653f };
        float[] expectedToleratedAmbientPressures = new float[COMPARTMENT_COUNT] { 1.4068f, 1.3544f, 1.1624f, 0.9923f, 0.8269f, 0.7455f, 0.6682f, 0.6059f, 0.5589f, 0.5471f, 0.5442f, 0.5459f, 0.5627f, 0.5592f, 0.5687f, 0.5794f };

        IDiveStage diveStage = new DiveStage()
        {
            DiveModel = new Zhl16Buhlmann()
            {
                DiveModelProfile = new DiveModelProfile(COMPARTMENT_COUNT)
                {
                    TotalTissuePressures = totalTissuePressures,
                    AValues = aValues,
                    BValues = bValues,
                }
            }
        };
        IDiveProfileStage diveProfileStage = new ToleratedAmbientPressures(diveStage);

        // When
        diveProfileStage.Run();

        // Then
        for (int i = 0; i < COMPARTMENT_COUNT; i++)
        {
            Assert.Equal(expectedToleratedAmbientPressures[i], diveStage.DiveModel.DiveModelProfile.ToleratedAmbientPressures[i], 4);
        }
    }
}