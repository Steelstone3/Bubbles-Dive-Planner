using ReactiveUI;

public class Main : ReactiveObject, IMain
{
    private IDiveModelSelector diveModelSelector = new DiveModelSelector();
    public IDiveModelSelector DiveModelSelector
    {
        get => diveModelSelector;
        set => this.RaiseAndSetIfChanged(ref diveModelSelector, value);
    }

    private IDivePlan divePlan = new DivePlan();
    public IDivePlan DivePlan
    {
        get => divePlan;
        set => this.RaiseAndSetIfChanged(ref divePlan, value);
    }
}

public interface IMain
{
    IDiveModelSelector DiveModelSelector { get; set; }
    IDivePlan DivePlan { get; set; }
}
