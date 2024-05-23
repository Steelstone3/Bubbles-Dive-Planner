public class AmbientPressures : IDiveProfileStage
{
    private IDiveStage diveStage;

    public AmbientPressures(IDiveStage diveStage)
    {
        this.diveStage = diveStage;
    }

    public void Run()
    {
        float ambientPressure = CalculateAmbientPressure();
        diveStage.DiveModel.DiveModelProfile.NitrogenAtPressure = CalculateAdjustedNitrogenPressure(ambientPressure);
        diveStage.DiveModel.DiveModelProfile.OxygenAtPressure = CalculateAdjustedOxygenPressure(ambientPressure);
        diveStage.DiveModel.DiveModelProfile.HeliumAtPressure = CalculateAdjustedHeliumPressure(ambientPressure);
    }

    private float CalculateAmbientPressure() => (float)(1.0 + (diveStage.DiveStep.Depth / 10.0));
    private float CalculateAdjustedNitrogenPressure(float pressureAmbient) => diveStage.GasMixture.Nitrogen / 100 * pressureAmbient;
    private float CalculateAdjustedOxygenPressure(float pressureAmbient) => diveStage.GasMixture.Oxygen / 100 * pressureAmbient;
    private float CalculateAdjustedHeliumPressure(float pressureAmbient) => diveStage.GasMixture.Helium / 100 * pressureAmbient;
}