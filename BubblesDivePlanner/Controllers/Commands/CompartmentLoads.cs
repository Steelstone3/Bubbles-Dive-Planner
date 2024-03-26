// TODO AH Test
public class CompartmentLoads : IDiveProfileStage
{
    private readonly IDiveModel diveModel;

    public CompartmentLoads(IDiveModel diveModel)
    {
        this.diveModel = diveModel;
    }

    public void Run()
    {
        for (int compartment = 0; compartment < diveModel.CompartmentCount; compartment++)
        {
            CalculateCompartmentLoad(compartment);
        }
    }

    private void CalculateCompartmentLoad(int compartment)
    {
        diveModel.DiveModelProfile.CompartmentLoads[compartment] = (float)Math.Round(diveModel.DiveModelProfile.TotalTissuePressures[compartment] / diveModel.DiveModelProfile.MaxSurfacePressures[compartment] * 100, 2);
    }
}