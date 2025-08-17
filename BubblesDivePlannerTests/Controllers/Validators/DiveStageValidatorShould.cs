using Xunit;

public class DiveStageValidatorShould
{
    [Fact]
    public void ValidateDiveStage()
    {
        // Given
        DiveStep diveStep = new()
        {
            Depth = 50,
            Time = 10,
        };
        GasMixture gasMixture = new()
        {
            Oxygen = 21
        };
        GasUsage gasUsage = new()
        {
            Remaining = 2400,
            Used = 0,
            SurfaceAirConsumptionRate = 12,
        };
        Cylinder cylinder = new()
        {
            Name = "Air",
            Pressure = 200,
            Volume = 12,
            GasMixture = gasMixture,
            GasUsage = gasUsage,
        };
        DiveStage diveStage = new()
        {
            DiveStep = diveStep,
            Cylinder = cylinder,
        };

        DiveStageValidator diveStageValidator = new();

        // When
        bool isValid = diveStageValidator.Validate(diveStage);

        // Then
        Assert.True(isValid);
    }
}