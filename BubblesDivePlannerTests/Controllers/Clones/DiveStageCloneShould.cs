using Moq;
using Xunit;

public class DiveStageCloneShould
{
    [Fact]
    public void Clone()
    {
        // Given
        Mock<IDiveStage> diveStage = new();
        DiveStageClone diveStageClone = new();

        // When
        IDiveStage newDiveStage = diveStageClone.Clone(diveStage.Object);

        // Then
        Assert.NotNull(newDiveStage);
        Assert.NotSame(diveStage.Object, newDiveStage);
    }
}