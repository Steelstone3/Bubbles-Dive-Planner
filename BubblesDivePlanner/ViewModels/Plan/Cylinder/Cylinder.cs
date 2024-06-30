using ReactiveUI;

public class Cylinder : ReactiveObject, ICylinder
{
    private readonly ICylinderValidator cylinderValidator;
    private readonly ICylinderController cylinderController;

    public Cylinder(ICylinderValidator cylinderValidator, ICylinderController cylinderController)
    {
        this.cylinderValidator = cylinderValidator;
        this.cylinderController = cylinderController;
    }

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
            InitialPressurisedVolume = cylinderController.CalculateInitialPressurisedVolume(volume, pressure);
        }
    }

    private ushort pressure;
    public ushort Pressure
    {
        get => pressure;
        set
        {
            this.RaiseAndSetIfChanged(ref pressure, value);
            InitialPressurisedVolume = cylinderController.CalculateInitialPressurisedVolume(volume, pressure);
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

    private IGasMixture gasMixture = new GasMixture(new GasMixtureValidator(), new CylinderController(), new DiveBoundaryController());
    public IGasMixture GasMixture
    {
        get => gasMixture;
        set => this.RaiseAndSetIfChanged(ref gasMixture, value);
    }

    private IGasUsage gasUsage = new GasUsage(new GasUsageValidator());
    public IGasUsage GasUsage
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

    public bool IsValid => cylinderValidator.Validate(this);
}

public interface ICylinder : IVisibility, IValidation
{
    string Name { get; set; }
    byte Volume { get; set; }
    ushort Pressure { get; set; }
    ushort InitialPressurisedVolume { get; set; }
    IGasMixture GasMixture { get; set; }
    IGasUsage GasUsage { get; set; }
}