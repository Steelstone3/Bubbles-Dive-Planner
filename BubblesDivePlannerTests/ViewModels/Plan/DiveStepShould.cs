using Moq;
using ReactiveUI;
using Xunit;

public class DiveStepShould
{
    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDiveStepValidator> diveStageValidator = new();
        DiveStep diveStep = new(diveStageValidator.Object);
        List<string> events = new();
        diveStep.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveStep.Depth = 50;
        diveStep.Time = 10;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveStep);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveStep.Depth), events);
        Assert.Contains(nameof(diveStep.Time), events);
    }

    [Fact]
    public void Validate()
    {
        // Given
        Mock<IDiveStepValidator> diveStageValidator = new();
        DiveStep diveStage = new(diveStageValidator.Object); ;
        diveStageValidator.Setup(dsv => dsv.Validate(diveStage)).Returns(true);

        // When
        bool isValid = diveStage.IsValid;

        // Then
        Assert.True(isValid);
        diveStageValidator.VerifyAll();
    }
}
