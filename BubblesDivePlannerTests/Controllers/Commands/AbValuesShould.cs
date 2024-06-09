using Moq;
using Xunit;

public class AbValuesShould
{
    [Fact]
    public void Run()
    {
        // Given
        const byte COMPARTMENT_COUNT = 16;
        float[] expectedAValues = new float[COMPARTMENT_COUNT] { 1.2559f, 1.0000f, 0.8618f, 0.7562f, 0.6667f, 0.5600f, 0.4947f, 0.4500f, 0.4187f, 0.3798f, 0.3497f, 0.3223f, 0.2850f, 0.2737f, 0.2523f, 0.2327f };
        float[] expectedBValues = new float[COMPARTMENT_COUNT] { 0.5050f, 0.6514f, 0.7222f, 0.7825f, 0.8126f, 0.8434f, 0.8693f, 0.8910f, 0.9092f, 0.9222f, 0.9319f, 0.9403f, 0.9477f, 0.9544f, 0.9602f, 0.9653f };
        Mock<IDiveStageValidator> diveStageValidator = new();
        IDiveStage diveStage = new DiveStage(diveStageValidator.Object)
        {
            DiveModel = new Zhl16Buhlmann(),
        };
        IDiveProfileStage diveProfileStage = new AbValues(diveStage);

        // When
        diveProfileStage.Run();

        // Then
        for (int i = 0; i < COMPARTMENT_COUNT; i++)
        {
            Assert.Equal(expectedAValues[i], diveStage.DiveModel.DiveModelProfile.AValues[i], 4);
            Assert.Equal(expectedBValues[i], diveStage.DiveModel.DiveModelProfile.BValues[i], 4);
        }
    }
}
