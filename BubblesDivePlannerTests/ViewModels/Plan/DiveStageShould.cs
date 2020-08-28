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
        Mock<IGasMixture> gasMixture = new();
        DiveStage diveStage = new();
        List<string> events = new();
        diveStage.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveStage.DiveModel = diveModel.Object;
        diveStage.DiveStep = diveStep.Object;
        diveStage.GasMixture = gasMixture.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveStage);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveStage.DiveModel), events);
        Assert.Contains(nameof(diveStage.DiveStep), events);
        Assert.Contains(nameof(diveStage.GasMixture), events);
    }
}