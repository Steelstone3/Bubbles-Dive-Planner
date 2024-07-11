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
}