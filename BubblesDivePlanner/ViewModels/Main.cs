using ReactiveUI;

public class Main : ReactiveObject, IMain
{
    public Main()
    {
        
    }

    private IDiveModelSelector diveModelSelector = new DiveModelSelector();
    public IDiveModelSelector DiveModelSelector
    {
        get => diveModelSelector;
        set => this.RaiseAndSetIfChanged(ref diveModelSelector, value);
    }

    private IDiveStage diveStage = new DiveStage();
    public IDiveStage DiveStage
    {
        get => diveStage;
        set => this.RaiseAndSetIfChanged(ref diveStage, value);
    }
}

public interface IMain
{
    IDiveModelSelector DiveModelSelector { get; set; }
    IDiveStage DiveStage { get; set; }
}
