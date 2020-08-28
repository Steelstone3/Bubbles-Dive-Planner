// TODO AH Test
public class ToleratedAmbientPressures : IDiveProfileStage
{
    private readonly IDiveModel diveModel;

    public ToleratedAmbientPressures(IDiveModel diveModel)
    {
        this.diveModel = diveModel;
    }

    public void Run()
    {
        for (int compartment = 0; compartment < diveModel.CompartmentCount; compartment++)
        {
            CalculateToleratedAmbientPressure(compartment);
        }
    }

    private void CalculateToleratedAmbientPressure(int compartment)
    {
        diveModel.DiveModelProfile.ToleratedAmbientPressures[compartment] = (float)Math.Round((diveModel.DiveModelProfile.TotalTissuePressures[compartment] - diveModel.DiveModelProfile.AValues[compartment]) * diveModel.DiveModelProfile.BValues[compartment], 4);
    }
}