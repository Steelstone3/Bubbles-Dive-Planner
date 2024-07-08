using Moq;
using Xunit;

public class JsonControllerShould
{
    [Fact]
    public void Serialise()
    {
        // Given
        Mock<IDiveStepValidator> diveStepValidator = new();
        DiveStep diveStep = new(diveStepValidator.Object)
        {
            Depth = 50,
            Time = 10,
        };
        Mock<IGasUsageValidator> gasUsageValidator = new();
        GasUsage gasUsage = new(gasUsageValidator.Object)
        {
            Remaining = 1680,
            Used = 720,
            SurfaceAirConsumptionRate = 12,
        };
        Mock<IGasMixtureValidator> gasMixtureValidator = new();
        Mock<ICylinderController> cylinderController = new();
        Mock<IDiveBoundaryController> diveBoundaryController = new();
        GasMixture gasMixture = new(gasMixtureValidator.Object, cylinderController.Object, diveBoundaryController.Object)
        {
            Oxygen = 21
        };
        Mock<ICylinderValidator> cylinderValidator = new();
        Cylinder cylinder = new(cylinderValidator.Object, cylinderController.Object)
        {
            Name = "Air",
            Volume = 12,
            Pressure = 200,
            InitialPressurisedVolume = 2400,
            GasMixture = gasMixture,
            GasUsage = gasUsage,
        };
        Mock<IDiveStageValidator> diveStageValidator = new();
        DiveStage diveStage = new(diveStageValidator.Object)
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
        Assert.Equal("[\n  {}\n]", serialisedResult);
    }
}