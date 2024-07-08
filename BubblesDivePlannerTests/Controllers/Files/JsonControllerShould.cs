using Moq;
using Xunit;

public class JsonControllerShould
{
    [Fact]
    public void Serialise()
    {
        // Given
        DiveStep diveStep = new(new DiveStepValidator())
        {
            Depth = 50,
            Time = 10,
        };
        GasMixture gasMixture = new(new GasMixtureValidator(), new CylinderController(), new DiveBoundaryController())
        {
            Oxygen = 21
        };
        GasUsage gasUsage = new(new GasUsageValidator())
        {
            Remaining = 1680,
            Used = 720,
            SurfaceAirConsumptionRate = 12,
        };
        Cylinder cylinder = new(new CylinderValidator(), new CylinderController())
        {
            Name = "Air",
            Volume = 12,
            Pressure = 200,
            InitialPressurisedVolume = 2400,
            GasMixture = gasMixture,
            GasUsage = gasUsage,
        };
        DiveStage diveStage = new(new DiveStageValidator())
        {
            DiveModel = new Zhl16Buhlmann(),
            DiveStep = diveStep,
            Cylinder = cylinder,
        };
        DivePlan divePlan=new()
        {
            DiveStage = diveStage
        };
        Main main = new()
        {
            DivePlan = divePlan,
        };
        JsonController jsonController = new();

        // When
        string serialisedResult = jsonController.Serialise(main);

        // Then
        Assert.Equal("{}", serialisedResult);
    }
}