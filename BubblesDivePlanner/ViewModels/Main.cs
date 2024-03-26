using ReactiveUI;

public class Main : ReactiveObject, IMain
{
    public IDiveModelSelector diveModelSelector;
    public IDiveModelSelector DiveModelSelector
    {
        get => diveModelSelector;
        set => this.RaiseAndSetIfChanged(ref diveModelSelector, value);
    }

    public IDivePlan divePlan;
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
