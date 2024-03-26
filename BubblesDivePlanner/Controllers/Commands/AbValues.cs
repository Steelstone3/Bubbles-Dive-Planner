// TODO AH Test
public class AbValues : IDiveProfileStage
{
    private readonly IDiveModel diveModel;

    public AbValues(IDiveModel diveModel)
    {
        this.diveModel = diveModel;
    }

    public void Run()
    {
        for (int compartment = 0; compartment < diveModel.CompartmentCount; compartment++)
        {
            CalculateAValues(compartment);
            CalculateBValues(compartment);
        }
    }

    private void CalculateAValues(int compartment)
    {
        diveModel.DiveModelProfile.AValues[compartment] = (float)Math.Round(((diveModel.AValuesNitrogen[compartment] * diveModel.DiveModelProfile.NitrogenTissuePressures[compartment]) + (diveModel.AValuesHelium[compartment] * diveModel.DiveModelProfile.HeliumTissuePressures[compartment])) / diveModel.DiveModelProfile.TotalTissuePressures[compartment], 4);
    }

    private void CalculateBValues(int compartment)
    {
        diveModel.DiveModelProfile.BValues[compartment] = (float)Math.Round(((diveModel.BValuesNitrogen[compartment] * diveModel.DiveModelProfile.NitrogenTissuePressures[compartment]) + (diveModel.BValuesHelium[compartment] * diveModel.DiveModelProfile.HeliumTissuePressures[compartment])) / diveModel.DiveModelProfile.TotalTissuePressures[compartment], 4);
    }
}
