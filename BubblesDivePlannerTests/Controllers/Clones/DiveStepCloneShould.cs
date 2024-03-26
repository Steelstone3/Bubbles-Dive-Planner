using Moq;
using Xunit;

public class DiveStepCloneShould
{
    [Fact]
    public void Clone()
    {
        // Given
        Mock<IDiveStep> diveStep = new();
        diveStep.Setup(diveStep => diveStep.Depth).Returns(50);
        diveStep.Setup(diveStep => diveStep.Time).Returns(10);
        DiveStepClone diveStepClone = new();

        // When
        IDiveStep newDiveStep = diveStepClone.Clone(diveStep.Object);

        // Then
        Assert.NotSame(diveStep.Object, newDiveStep);
    }
}