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
        FileController fileController = new(cylinderSelectorSerialiser.Object, resultSerialiser.Object);

        // When
        fileController.Write(cylinderSelector.Object, result.Object);

        // Then
        cylinderSelectorSerialiser.Verify(css => css.Write(cylinderSelector.Object));
        resultSerialiser.Verify(rs => rs.Write(result.Object));
    }
}