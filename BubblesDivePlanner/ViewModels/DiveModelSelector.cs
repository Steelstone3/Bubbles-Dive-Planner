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
}

public interface IDiveModelSelector
{
    IDiveModel DiveModelSelected { get; set; }
}