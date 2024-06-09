using ReactiveUI;

public class DiveModelSelector : ReactiveObject, IDiveModelSelector
{
    public IList<IDiveModel> DiveModels => new List<IDiveModel>
    {
        new Zhl16Buhlmann(),
        new UsnRevisionSix(),
    };

    private IDiveModel diveModelSelected;
    public IDiveModel DiveModelSelected
    {
        get => diveModelSelected;
        set => this.RaiseAndSetIfChanged(ref diveModelSelected, value);
    }

    private bool isVisible = true;
    public bool IsVisible
    {
        get => isVisible;
        set => this.RaiseAndSetIfChanged(ref isVisible, value);
    }
}

public interface IDiveModelSelector : IVisibility
{
    IList<IDiveModel> DiveModels { get; }
    IDiveModel DiveModelSelected { get; set; }
}