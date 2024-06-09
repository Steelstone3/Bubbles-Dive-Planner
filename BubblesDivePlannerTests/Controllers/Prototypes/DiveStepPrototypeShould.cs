using Moq;
using Xunit;

public class DiveStepPrototypeShould
{
    private readonly IDiveStepPrototype diveStepPrototype = new DiveStepPrototype();

    [Fact]
    public void DeepClone()
    {
        // Given
        Mock<IDiveStepValidator> diveStepValidator = new();
        IDiveStep diveStep = new DiveStep(diveStepValidator.Object);

        // When
        IDiveStep clonedDiveStep = diveStepPrototype.DeepClone(diveStep);

        // Then
        Assert.NotSame(diveStep, clonedDiveStep);
    }
}