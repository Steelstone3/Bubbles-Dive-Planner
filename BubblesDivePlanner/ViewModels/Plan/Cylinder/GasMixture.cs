using ReactiveUI;

public class GasMixture : ReactiveObject
{
    private readonly IDiveBoundaryController diveBoundaryController;

    // TODO AH remove controller polution
    public GasMixture()
    {
        diveBoundaryController = new DiveBoundaryController();
    }

    private float oxygen;
    public float Oxygen
    {
        get => oxygen;
        set
        {
            this.RaiseAndSetIfChanged(ref oxygen, value);
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
        private set => this.RaiseAndSetIfChanged(ref maximumOperatingDepth, value);
    }
}
