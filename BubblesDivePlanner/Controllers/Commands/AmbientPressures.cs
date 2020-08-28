// TODO AH Test
public class AmbientPressures : IDiveProfileStage
{
    private readonly IDiveModelProfile diveModelProfile;
    private readonly IGasMixture gasMixture;
    private readonly IDiveStep diveStep;

    public AmbientPressures(IDiveModelProfile diveModelProfile, IGasMixture gasMixture, IDiveStep diveStep)
    {
        this.diveModelProfile = diveModelProfile;
        this.gasMixture = gasMixture;
        this.diveStep = diveStep;
    }

    public void Run()
    {
        var pressureAmbient = CalculateAmbientPressure();
        CalculateAdjustedGasPressures(pressureAmbient);
    }

    private double CalculateAmbientPressure()
    {
        return 1.0 + (diveStep.Depth / 10.0);
    }

    private void CalculateAdjustedGasPressures(double pressureAmbient)
    {
        diveModelProfile.NitrogenAtPressure = (float)(gasMixture.Nitrogen / 100 * pressureAmbient);
        diveModelProfile.OxygenAtPressure = (float)(gasMixture.Oxygen / 100 * pressureAmbient);
        diveModelProfile.HeliumAtPressure = (float)(gasMixture.Helium / 100 * pressureAmbient);
    }
}