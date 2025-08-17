using ReactiveUI;

public class GasMixture : ReactiveObject
{
    public GasMixture(GasMixture gasMixture)
    {
        Oxygen = gasMixture.Oxygen;
        Helium = gasMixture.Helium;
        Nitrogen = gasMixture.Nitrogen;
        MaximumOperatingDepth = gasMixture.MaximumOperatingDepth;
    }

    public GasMixture() { }

    private float oxygen;
    public float Oxygen
    {
        get => oxygen;
        set
        {
            this.RaiseAndSetIfChanged(ref oxygen, value);
        }
    }

    private float helium;
    public float Helium
    {
        get => helium;
        set
        {
            this.RaiseAndSetIfChanged(ref helium, value);
        }
    }


    private float nitrogen = 100;
    public float Nitrogen
    {
        get => nitrogen;
        set => this.RaiseAndSetIfChanged(ref nitrogen, value);
    }

    private float maximumOperatingDepth;
    public float MaximumOperatingDepth
    {
        get => maximumOperatingDepth;
        set => this.RaiseAndSetIfChanged(ref maximumOperatingDepth, value);
    }
}
