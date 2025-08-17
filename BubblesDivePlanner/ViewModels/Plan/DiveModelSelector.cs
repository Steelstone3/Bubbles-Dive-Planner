using ReactiveUI;

public class DiveModelSelector : ReactiveObject, IVisibility
{
    private DiveModelFactory diveModelFactory = new();

    public List<DiveModel> DiveModels => new List<DiveModel>
    {
        diveModelFactory.CreateZhl16Buhlmann(),
        diveModelFactory.CreateUsnRevisionSix(),
    };

    private DiveModel diveModelSelected;
    public DiveModel DiveModelSelected
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