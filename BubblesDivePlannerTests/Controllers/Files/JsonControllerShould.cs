using System.Collections.ObjectModel;
using Moq;
using Xunit;

public class JsonControllerShould
{
    [Fact(Skip = "Later")]
    public void Serialise()
    {
        // Given
        Mock<IDiveStep> diveStep = new();
        diveStep.Setup(ds => ds.Depth).Returns(50);
        diveStep.Setup(ds => ds.Time).Returns(10);
        Mock<IGasMixture> gasMixture = new();
        gasMixture.Setup(gm => gm.Oxygen).Returns(21);
        gasMixture.Setup(gm => gm.Nitrogen).Returns(79);
        Mock<ICylinder> cylinder = new();
        cylinder.Setup(c => c.Name).Returns("Air");
        cylinder.Setup(c => c.Volume).Returns(12);
        cylinder.Setup(c => c.Pressure).Returns(200);
        cylinder.Setup(c => c.InitialPressurisedVolume).Returns(2400);
        Mock<IDiveStage> diveStage = new();
        diveStage.Setup(ds => ds.DiveModel).Returns(new Zhl16Buhlmann());
        diveStage.Setup(ds => ds.DiveStep).Returns(diveStep.Object);
        diveStage.Setup(ds => ds.Cylinder).Returns(cylinder.Object);
        Mock<Result> result = new();
        result.Setup(r => r.Results).Returns(new ObservableCollection<IDiveStage>() { diveStage.Object });
        JsonController jsonController = new();

        // When
        string serialisedResult = jsonController.Serialise(result.Object);

        // Then
        Assert.Equal("[]", serialisedResult);
    }
}