using Moq;
using Xunit;

public class FileControllerShould
{
    [Fact]
    public void Save()
    {
        // Given
        Mock<ICylinderSelector> cylinderSelector = new();
        Mock<ISerialiser<ICylinderSelector>> cylinderSelectorSerialiser = new();
        cylinderSelectorSerialiser.Setup(css => css.Write(cylinderSelector.Object)).Returns("Cylinders");
        Mock<IResult> result = new();
        Mock<ISerialiser<IResult>> resultSerialiser = new();
        resultSerialiser.Setup(rs => rs.Write(result.Object)).Returns("Results");
        Mock<IDivePlan> divePlan = new();
        divePlan.Setup(dp => dp.CylinderSelector).Returns(cylinderSelector.Object);
        Mock<IMain> main = new();
        main.Setup(m => m.DivePlan).Returns(divePlan.Object);
        main.Setup(m => m.Result).Returns(result.Object);
        FileController fileController = new(cylinderSelectorSerialiser.Object, resultSerialiser.Object);

        // When
        fileController.Write(main.Object);

        // Then
        cylinderSelectorSerialiser.Verify(css => css.Write(cylinderSelector.Object));
        resultSerialiser.Verify(rs => rs.Write(result.Object));
    }

    [Fact(Skip = "Might not be testable")]
    public void Read()
    {
        // Given
        Mock<ICylinderSelector> cylinderSelector = new();
        Mock<ISerialiser<ICylinderSelector>> cylinderSelectorSerialiser = new();
        string cylinderSelectorJson = "{\"Cylinders\":[{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}}],\"SetupCylinder\":{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}},\"SelectedCylinder\":{\"Name\":\"Air\",\"Volume\":12,\"Pressure\":200,\"InitialPressurisedVolume\":2400,\"GasMixture\":{\"Oxygen\":21,\"Helium\":0},\"GasUsage\":{\"Remaining\":1680,\"Used\":720,\"SurfaceAirConsumptionRate\":12}}}";
        cylinderSelectorSerialiser.Setup(css => css.Read(cylinderSelectorJson)).Returns(cylinderSelector.Object);
        Mock<IResult> result = new();
        Mock<ISerialiser<IResult>> resultSerialiser = new();
        resultSerialiser.Setup(rs => rs.Read("Results")).Returns(result.Object);
        Mock<IDivePlan> divePlan = new();
        divePlan.Setup(dp => dp.CylinderSelector).Returns(cylinderSelector.Object);
        Mock<IMain> main = new();
        main.Setup(m => m.DivePlan).Returns(divePlan.Object);
        main.Setup(m => m.Result).Returns(result.Object);
        FileController fileController = new(cylinderSelectorSerialiser.Object, resultSerialiser.Object);

        // When
        fileController.Read(main.Object);

        // Then
        cylinderSelectorSerialiser.Verify(css => css.Read(cylinderSelectorJson));
        resultSerialiser.Verify(rs => rs.Read("Results"));
    }
}