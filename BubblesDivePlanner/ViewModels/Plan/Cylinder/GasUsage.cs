using ReactiveUI;

public class GasUsage : ReactiveObject
{
    public GasUsage(GasUsage gasUsage)
    {
        Remaining = gasUsage.Remaining;
        Used = gasUsage.Used;
        SurfaceAirConsumptionRate = gasUsage.SurfaceAirConsumptionRate;
    }

    public GasUsage() { }

    private ushort remaining;
    public ushort Remaining
    {
        get => remaining;
        set => this.RaiseAndSetIfChanged(ref remaining, value);
    }

    private ushort used;
    public ushort Used
    {
        get => used;
        set => this.RaiseAndSetIfChanged(ref used, value);
    }

    private byte surfaceAirConsumptionRate;
    public byte SurfaceAirConsumptionRate
    {
        get => surfaceAirConsumptionRate;
        set => this.RaiseAndSetIfChanged(ref surfaceAirConsumptionRate, value);
    }
}
