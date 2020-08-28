using Moq;
using Xunit;

public class DiveStageCloneShould
{
    [Fact]
    public void Clone()
    {
        // Given
        Mock<IDiveModel> diveModel = new();
        diveModel.Setup(diveModel => diveModel.DiveModelProfile).Returns(new Mock<IDiveModelProfile>().Object);
        Mock<IDiveStage> diveStage = new();
        diveStage.Setup(diveStage => diveStage.DiveModel).Returns(diveModel.Object);
        diveStage.Setup(diveStage => diveStage.DiveStep).Returns(new Mock<IDiveStep>().Object);
        diveStage.Setup(diveStage => diveStage.GasMixture).Returns(new Mock<IGasMixture>().Object);
        DiveStageClone diveStageClone = new();

        // When
        IDiveStage newDiveStage = diveStageClone.Clone(diveStage.Object);

        // Then
        Assert.NotNull(newDiveStage);
        Assert.NotSame(diveStage.Object, newDiveStage);
    }
}