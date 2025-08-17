using ReactiveUI;

public class GasMixture : ReactiveObject
{
    private readonly ICylinderController cylinderController;
    private readonly IDiveBoundaryController diveBoundaryController;

    // TODO AH remove controller polution
    public GasMixture()
    {
        cylinderController = new CylinderController();
        diveBoundaryController = new DiveBoundaryController();
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
}
