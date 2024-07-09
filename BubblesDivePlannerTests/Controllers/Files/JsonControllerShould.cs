using System.Runtime.InteropServices;
using Moq;
using Xunit;

public class JsonControllerShould
{
    [SkippableFact]
    public void Serialise()
    {
        Skip.If(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

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
        Result result = new();

        result.Results.Add(diveStage);

        JsonController jsonController = new();

        // When
        string serialisedResult = jsonController.Serialise(result);

        // Then
        Assert.Equal("{\n  \"Results\": [\n    {}\n  ]\n}", serialisedResult);
    }
}