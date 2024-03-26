using Moq;
using ReactiveUI;
using Xunit;

public class DiveModelSelectorShould
{

    [Fact]
    public void Construct()
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
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveModelSelector.DiveModelSelected), events);
    }
}

internal class DiveModelSelector : ReactiveObject, IDiveModelSelector
{
    public IList<IDiveModel> DiveModels => new List<IDiveModel>
    {
        new Zhl16BuhlmannModel(),
        new UsnRevision6Model(),
    };

    private IDiveModel diveModelSelected;
    public IDiveModel DiveModelSelected
    {
        get => diveModelSelected;
        set => this.RaiseAndSetIfChanged(ref diveModelSelected, value);
    }
}

internal class UsnRevision6Model : IDiveModel
{
}

internal class Zhl16BuhlmannModel : IDiveModel
{
}

internal interface IDiveModelSelector
{
    IDiveModel DiveModelSelected { get; set; }
}

public interface IDiveModel
{
}