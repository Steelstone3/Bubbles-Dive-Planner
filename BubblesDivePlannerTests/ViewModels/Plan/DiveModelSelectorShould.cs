using Moq;
using ReactiveUI;
using Xunit;

public class DiveModelSelectorShould
{
    [Fact]
    public void HaveDefaults()
    {
        // Given
        DiveModelSelector diveModelSelector = new();

        // Then
        Assert.NotEmpty(diveModelSelector.DiveModels);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDiveModel> diveModel = new();
        DiveModelSelector diveModelSelector = new();
        List<string> events = new();
        diveModelSelector.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveModelSelector.DiveModelSelected = diveModel.Object;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveModelSelector);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveModelSelector.DiveModelSelected), events);
    }
}
