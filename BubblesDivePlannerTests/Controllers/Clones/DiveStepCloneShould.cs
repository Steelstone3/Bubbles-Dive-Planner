using Moq;
using Xunit;

public class DiveStepCloneShould
{
    [Fact]
    public void Clone()
    {
        // Given
        Mock<IDiveStep> diveStep = new();
        DiveStepClone diveStepClone = new();

        // When
        IDiveStep newDiveStep = diveStepClone.Clone(diveStep.Object);

        // Then
        Assert.NotNull(newDiveStep);
        Assert.NotSame(diveStep.Object, newDiveStep);
    }
}