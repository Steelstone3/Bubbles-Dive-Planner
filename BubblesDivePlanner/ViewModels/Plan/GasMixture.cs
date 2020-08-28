using ReactiveUI;

public class GasMixture : ReactiveObject, IGasMixture
{
    private readonly IGasMixtureValidator gasMixtureValidator = new GasMixtureValidator();

    private float oxygen;
    public float Oxygen
    {
        get => oxygen;
        set
        {
            this.RaiseAndSetIfChanged(ref oxygen, value);
            Nitrogen = gasMixtureValidator.CalculateNitrogen(this);
        }
    }

    private float helium;
    public float Helium
    {
        get => helium;
        set
        {
            this.RaiseAndSetIfChanged(ref helium, value);
            Nitrogen = gasMixtureValidator.CalculateNitrogen(this);
        }
    }

    private float nitrogen = 100;
    public float Nitrogen
    {
        get => nitrogen;
        private set => this.RaiseAndSetIfChanged(ref nitrogen, value);
    }

    // TODO AH Test
    public bool IsValid => gasMixtureValidator.Validate(this);
}

public interface IGasMixture : IValidation
{
    float Oxygen { get; set; }
    float Helium { get; set; }
    float Nitrogen { get; }
}