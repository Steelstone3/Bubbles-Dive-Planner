using ReactiveUI;

public class DiveStep : ReactiveObject
{
    public DiveStep(DiveStep diveStep)
    {
        Depth = diveStep.Depth;
        Time = diveStep.Time;
    }

    public DiveStep() { }

    private byte _depth;
    public byte Depth
    {
        get => _depth;
        set => this.RaiseAndSetIfChanged(ref _depth, value);
    }

    private byte _time;
    public byte Time
    {
        get => _time;
        set => this.RaiseAndSetIfChanged(ref _time, value);
    }
}
