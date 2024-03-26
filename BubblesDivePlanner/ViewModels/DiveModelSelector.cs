using ReactiveUI;

public class DiveModelSelector : ReactiveObject, IDiveModelSelector
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

public interface IDiveModelSelector
{
    IDiveModel DiveModelSelected { get; set; }
}