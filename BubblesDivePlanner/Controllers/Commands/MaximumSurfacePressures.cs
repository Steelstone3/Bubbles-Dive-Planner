public class MaximumSurfacePressures : IDiveProfileStage
{
    private IDiveStage diveStage;

    public MaximumSurfacePressures(IDiveStage diveStage)
    {
        this.diveStage = diveStage;
    }

    public void Run()
    {
        float[] newMaxSurfacePressures = new float[diveStage.DiveModel.CompartmentCount];

        for (int compartment = 0; compartment < diveStage.DiveModel.CompartmentCount; compartment++)
        {
            newMaxSurfacePressures[compartment] = CalculateMaximumSurfacePressure(compartment);
        }

        diveStage.DiveModel.DiveModelProfile.MaxSurfacePressures = newMaxSurfacePressures;
    }

    private float CalculateMaximumSurfacePressure(int compartment) => (float)Math.Round((1.0f / diveStage.DiveModel.DiveModelProfile.BValues[compartment]) + diveStage.DiveModel.DiveModelProfile.AValues[compartment], 4);
}