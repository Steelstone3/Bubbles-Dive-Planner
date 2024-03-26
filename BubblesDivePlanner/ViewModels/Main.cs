using ReactiveUI;

public class Main : ReactiveObject, IMain
{
    public IDivePlan divePlan;
    public IDivePlan DivePlan
    {
        get => divePlan;
        set => this.RaiseAndSetIfChanged(ref divePlan, value);
    }
}

public interface IMain
{
    IDivePlan DivePlan { get; set; }
}
