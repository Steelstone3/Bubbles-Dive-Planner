using Moq;
using ReactiveUI;
using Xunit;

public class DiveStageShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDiveModel> diveModel = new();
        Mock<IDiveStep> diveStep = new();
        Mock<ICylinder> cylinder = new();
        Mock<IDiveStageValidator> diveStageValidator = new();
        DiveStage diveStage = new(diveStageValidator.Object);
        List<string> events = new();
        diveStage.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveStage.DiveModel = diveModel.Object;
        diveStage.DiveStep = diveStep.Object;
        diveStage.Cylinder = cylinder.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveStage);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveStage.DiveModel), events);
        Assert.Contains(nameof(diveStage.DiveStep), events);
        Assert.Contains(nameof(diveStage.Cylinder), events);
    }

    [Fact]
    public void Validate()
    {
        // Given
        Mock<IDiveStageValidator> diveStageValidator = new();
        DiveStage diveStage = new(diveStageValidator.Object); ;
        diveStageValidator.Setup(dsv => dsv.Validate(diveStage)).Returns(true);

        // When
        bool isValid = diveStage.IsValid;

        // Then
        Assert.True(isValid);
        diveStageValidator.VerifyAll();
    }
}