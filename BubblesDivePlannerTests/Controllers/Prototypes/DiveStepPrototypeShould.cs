using Moq;
using Xunit;

public class DiveStepPrototypeShould
{
    private readonly IDiveStepPrototype diveStepPrototype = new DiveStepPrototype();

    [Fact]
    public void DeepClone()
    {
        // Given
        DiveStep diveStep = new DiveStep();

        // When
        DiveStep clonedDiveStep = diveStepPrototype.DeepClone(diveStep);

        // Then
        Assert.NotSame(diveStep, clonedDiveStep);
    }
}