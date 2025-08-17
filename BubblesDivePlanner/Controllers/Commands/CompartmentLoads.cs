public class CompartmentLoads : IDiveProfileStage
{
    private DiveStage diveStage;

    public CompartmentLoads(DiveStage diveStage)
    {
        this.diveStage = diveStage;
    }

    public void Run()
    {
        float[] newCompartmentLoads = new float[diveStage.DiveModel.CompartmentCount];

        for (int compartment = 0; compartment < diveStage.DiveModel.CompartmentCount; compartment++)
        {
            newCompartmentLoads[compartment] = CalculateCompartmentLoad(compartment);
        }

        diveStage.DiveModel.DiveModelProfile.CompartmentLoads = newCompartmentLoads;
    }

    private float CalculateCompartmentLoad(int compartment) => (float)Math.Round(diveStage.DiveModel.DiveModelProfile.TotalTissuePressures[compartment] / diveStage.DiveModel.DiveModelProfile.MaxSurfacePressures[compartment] * 100, 2);
}