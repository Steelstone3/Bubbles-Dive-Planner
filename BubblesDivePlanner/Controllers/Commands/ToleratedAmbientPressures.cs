public class ToleratedAmbientPressures : IDiveProfileStage
{
    private DiveStage diveStage;

    public ToleratedAmbientPressures(DiveStage diveStage)
    {
        this.diveStage = diveStage;
    }

    public void Run()
    {
        float[] newToleratedAmbientPressures = new float[diveStage.DiveModel.CompartmentCount];

        for (int compartment = 0; compartment < diveStage.DiveModel.CompartmentCount; compartment++)
        {
            newToleratedAmbientPressures[compartment] = CalculateToleratedAmbientPressure(compartment);
        }

        diveStage.DiveModel.DiveModelProfile.ToleratedAmbientPressures = newToleratedAmbientPressures;
    }

    private float CalculateToleratedAmbientPressure(int compartment) => (float)Math.Round((diveStage.DiveModel.DiveModelProfile.TotalTissuePressures[compartment] - diveStage.DiveModel.DiveModelProfile.AValues[compartment]) * diveStage.DiveModel.DiveModelProfile.BValues[compartment], 4);
}