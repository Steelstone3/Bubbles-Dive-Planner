using ReactiveUI;

public class GasMixture : ReactiveObject, IGasMixture
{
    private readonly IGasMixtureValidator gasMixtureValidator;
    private readonly ICylinderController cylinderController;

    public GasMixture(IGasMixtureValidator gasMixtureValidator, ICylinderController cylinderController)
    {
        this.gasMixtureValidator = gasMixtureValidator;
        this.cylinderController = cylinderController;
    }

    private float oxygen;
    public float Oxygen
    {
        get => oxygen;
        set
        {
            this.RaiseAndSetIfChanged(ref oxygen, value);
            Nitrogen = cylinderController.CalculateNitrogen(Oxygen, Helium);
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

    private bool isVisibile;
    public bool IsVisible
    {
        get => isVisibile;
        set => this.RaiseAndSetIfChanged(ref isVisibile, value);
    }

    public bool IsValid => gasMixtureValidator.Validate(this);
}

public interface IGasMixture : IVisibility, IValidation
{
    float Oxygen { get; set; }
    float Helium { get; set; }
    float Nitrogen { get; }
}