using Moq;
using Xunit;

public class DiveModelCloneShould
{
    [Fact]
    public void Clone()
    {
        // Given
        Mock<IDiveModel> diveModel = new();
        DiveModelClone diveModelClone = new();

        // When
        IDiveModel newDiveModel = diveModelClone.Clone(diveModel.Object);

        // Then
        Assert.NotNull(newDiveModel);
        Assert.NotSame(diveModel.Object, newDiveModel);
    }
}