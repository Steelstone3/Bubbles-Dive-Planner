using Moq;
using Xunit;

public class DiveStepValidatorShould
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(50, 10)]
    [InlineData(100, 60)]
    public void ValidateValidDiveStep(byte depth, byte time)
    {
        // Given
        Mock<IDiveStep> diveStep = new();
        diveStep.Setup(diveStep => diveStep.Depth).Returns(depth);
        diveStep.Setup(diveStep => diveStep.Time).Returns(time);
        DiveStepValidator diveStepValidator = new();

        // When
        bool isValid = diveStepValidator.Validate(diveStep.Object);

        // Then
        Assert.True(isValid);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 61)]
    [InlineData(101, 0)]
    [InlineData(101, 61)]
    public void ValidateInvalidDiveStep(byte depth, byte time)
    {
        // Given
        Mock<IDiveStep> diveStep = new();
        diveStep.Setup(diveStep => diveStep.Depth).Returns(depth);
        diveStep.Setup(diveStep => diveStep.Time).Returns(time);
        DiveStepValidator diveStepValidator = new();

        // When
        bool isValid = diveStepValidator.Validate(diveStep.Object);

        // Then
        Assert.False(isValid);
    }
}