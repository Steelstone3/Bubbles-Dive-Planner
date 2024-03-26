using Moq;
using Xunit;

public class DiveProfileStagesFactoryShould
{
    private readonly Mock<IDiveModel> diveModel = new();
    private readonly Mock<IDiveModelProfile> diveModelProfile = new();
    private readonly Mock<IDiveStage> diveStage = new();

    [Fact]
    public void Create()
    {
        // Given
        CreateStub();
        DiveProfileStagesFactory diveProfileStagesFactory = new();

        // When
        IDiveProfileStage[] diveProfileStages = diveProfileStagesFactory.Create(diveStage.Object);

        // Then
        Assert.NotEmpty(diveProfileStages);
    }

    [Fact]
    public void Run()
    {
        // Given
        CreateStub();
        DiveProfileStagesFactory diveProfileStagesFactory = new();

        // When
        IDiveProfileStage[] diveProfileStages = diveProfileStagesFactory.Create(diveStage.Object);

        // Then
        Assert.NotEmpty(diveProfileStages);
    }

    private void CreateStub()
    {
        diveModel.Setup(diveModel => diveModel.DiveModelProfile).Returns(diveModelProfile.Object);
        diveStage.Setup(diveStage => diveStage.DiveModel).Returns(diveModel.Object);
        diveStage.Setup(diveStage => diveStage.DiveStep).Returns(new Mock<IDiveStep>().Object);
        diveStage.Setup(diveStage => diveStage.GasMixture).Returns(new Mock<IGasMixture>().Object);
    }
}