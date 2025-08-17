using ReactiveUI;

public class Cylinder : ReactiveObject, IVisibility
{
    public Cylinder(Cylinder cylinder)
    {
        Name = cylinder.Name;
        Volume = cylinder.Volume;
        Pressure = cylinder.Pressure;
        InitialPressurisedVolume = cylinder.InitialPressurisedVolume;
        GasMixture = new GasMixture(cylinder.GasMixture);
        GasUsage = new GasUsage(cylinder.GasUsage);
    }

    public Cylinder() { }

    private string name = "Air";
    public string Name
    {
        get => name;
        set => this.RaiseAndSetIfChanged(ref name, value);
    }

    private byte volume;
    public byte Volume
    {
        get => volume;
        set
        {
            this.RaiseAndSetIfChanged(ref volume, value);
        }
    }

    private ushort pressure;
    public ushort Pressure
    {
        get => pressure;
        set
        {
            this.RaiseAndSetIfChanged(ref pressure, value);
        }
    }

    private ushort initialPressurisedVolume;
    public ushort InitialPressurisedVolume
    {
        get => initialPressurisedVolume;
        set
        {
            this.RaiseAndSetIfChanged(ref initialPressurisedVolume, value);
            GasUsage.Remaining = initialPressurisedVolume;
        }
    }

    private GasMixture gasMixture = new();
    public GasMixture GasMixture
    {
        get => gasMixture;
        set => this.RaiseAndSetIfChanged(ref gasMixture, value);
    }

    private GasUsage gasUsage = new GasUsage();
    public GasUsage GasUsage
    {
        get => gasUsage;
        set => this.RaiseAndSetIfChanged(ref gasUsage, value);
    }

    private bool isVisibile = true;
    public bool IsVisible
    {
        get => isVisibile;
        set => this.RaiseAndSetIfChanged(ref isVisibile, value);
    }
}