// TODO AH Test
public class MaximumSurfacePressures : IDiveProfileStage
{
    private readonly IDiveModel diveModel;

    public MaximumSurfacePressures(IDiveModel diveModel)
    {
        this.diveModel = diveModel;
    }

    public void Run()
    {
        for (int compartment = 0; compartment < diveModel.CompartmentCount; compartment++)
        {
            CalculateMaximumSurfacePressure(compartment);
        }
    }

    private void CalculateMaximumSurfacePressure(int compartment)
    {
        diveModel.DiveModelProfile.MaxSurfacePressures[compartment] = (float)Math.Round((1.0f / diveModel.DiveModelProfile.BValues[compartment]) + diveModel.DiveModelProfile.AValues[compartment], 4);
    }
}