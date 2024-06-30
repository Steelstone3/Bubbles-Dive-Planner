using ReactiveUI;

public class GasMixture : ReactiveObject, IGasMixture
{
    private readonly IGasMixtureValidator gasMixtureValidator;
    private readonly ICylinderController cylinderController;
    private readonly IDiveBoundaryController diveBoundaryController;

    public GasMixture(IGasMixtureValidator gasMixtureValidator, ICylinderController cylinderController, IDiveBoundaryController diveBoundaryController)
    {
        this.gasMixtureValidator = gasMixtureValidator;
        this.cylinderController = cylinderController;
        this.diveBoundaryController = diveBoundaryController;
    }

    private float oxygen;
    public float Oxygen
    {
        get => oxygen;
        set
        {
            this.RaiseAndSetIfChanged(ref oxygen, value);
            Nitrogen = cylinderController.CalculateNitrogen(Oxygen, Helium);
            MaximumOperatingDepth = diveBoundaryController.CalculateMaximumOperatingDepth(Oxygen);
        }
    }

    private float helium;
    public float Helium
    {
        get => helium;
        set
        {
            this.RaiseAndSetIfChanged(ref helium, value);
            Nitrogen = cylinderController.CalculateNitrogen(Oxygen, Helium);
        }
    }


    private float nitrogen = 100;
    public float Nitrogen
    {
        get => nitrogen;
        private set => this.RaiseAndSetIfChanged(ref nitrogen, value);
    }

    private float maximumOperatingDepth;
    public float MaximumOperatingDepth
    {
        get => maximumOperatingDepth;
        private set => this.RaiseAndSetIfChanged(ref maximumOperatingDepth, value);
    }

    public bool IsValid => gasMixtureValidator.Validate(this);
}

public interface IGasMixture : IValidation
{
    float Oxygen { get; set; }
    float Helium { get; set; }
    float Nitrogen { get; }
    float MaximumOperatingDepth { get; }
}