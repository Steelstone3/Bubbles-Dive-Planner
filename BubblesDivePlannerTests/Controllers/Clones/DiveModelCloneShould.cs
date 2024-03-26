using Moq;
using Xunit;

public class DiveModelCloneShould
{
    [Fact]
    public void Clone()
    {
        // Given
        Mock<IDiveModel> diveModel = new();
        diveModel.Setup(diveModel => diveModel.DiveModelProfile).Returns(new Mock<IDiveModelProfile>().Object);
        DiveModelClone diveModelClone = new();

        // When
        IDiveModel newDiveModel = diveModelClone.Clone(diveModel.Object);

        // Then
        Assert.NotNull(newDiveModel);
        Assert.NotSame(diveModel.Object, newDiveModel);
    }
}