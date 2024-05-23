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
        IDiveStage diveStage = new DiveStage()
        {
            DiveModel = new Zhl16Buhlmann(),
            DiveStep = new DiveStep()
            {
                Depth = 50,
                Time = 10,
            },
            GasMixture = new GasMixture()
            {
                Oxygen = 21,
                Helium = 0,
            }
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