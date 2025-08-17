using Moq;
using Xunit;

public class FileControllerShould
{
    [Fact]
    public void Save()
    {
        // Given
        CylinderSelector cylinderSelector = new();
        Mock<ISerialiser<CylinderSelector>> cylinderSelectorSerialiser = new();
        cylinderSelectorSerialiser.Setup(css => css.Write(cylinderSelector)).Returns("Cylinders");
        Result result = new();
        Mock<ISerialiser<Result>> resultSerialiser = new();
        resultSerialiser.Setup(rs => rs.Write(result)).Returns("Results");
        DivePlan divePlan = new()
        {
            CylinderSelector = cylinderSelector
        };
        Main main = new();
        main.DivePlan = divePlan;
        main.Result = result;
        FileController fileController = new(cylinderSelectorSerialiser.Object, resultSerialiser.Object);

        // When
        fileController.Write(main);

        // Then
        cylinderSelectorSerialiser.Verify(css => css.Write(cylinderSelector));
        resultSerialiser.Verify(rs => rs.Write(result));
    }

    [Fact(Skip = "Might not be testable")]
    public void Read()
    {
        // Given
        CylinderSelector cylinderSelector = new();
        Mock<ISerialiser<CylinderSelector>> cylinderSelectorSerialiser = new();
        string cylinderSelectorJson = "{\"Cylinders\":[{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}}],\"SetupCylinder\":{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}},\"SelectedCylinder\":{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}}}";
        cylinderSelectorSerialiser.Setup(css => css.Read(cylinderSelectorJson)).Returns(cylinderSelector);
        Result result = new();
        Mock<ISerialiser<Result>> resultSerialiser = new();
        resultSerialiser.Setup(rs => rs.Read("Results")).Returns(result);
        DivePlan divePlan = new()
        {
            CylinderSelector = cylinderSelector
        };
        Main main = new();
        main.DivePlan = divePlan;
        main.Result = result;
        FileController fileController = new(cylinderSelectorSerialiser.Object, resultSerialiser.Object);

        // When
        fileController.Read(main);

        // Then
        cylinderSelectorSerialiser.Verify(css => css.Read(cylinderSelectorJson));
        resultSerialiser.Verify(rs => rs.Read("Results"));
    }
}