public class AbValues : IDiveProfileStage
{
    private DiveStage diveStage;

    public AbValues(DiveStage diveStage)
    {
        this.diveStage = diveStage;
    }

    public void Run()
    {
        float[] newAValues = new float[diveStage.DiveModel.CompartmentCount];
        float[] newBValues = new float[diveStage.DiveModel.CompartmentCount];

        for (int compartment = 0; compartment < diveStage.DiveModel.CompartmentCount; compartment++)
        {
            newAValues[compartment] = CalculateAValues(compartment);
            newBValues[compartment] = CalculateBValues(compartment);
        }

        diveStage.DiveModel.DiveModelProfile.AValues = newAValues;
        diveStage.DiveModel.DiveModelProfile.BValues = newBValues;
    }

    private float CalculateAValues(int compartment) => (float)Math.Round(((diveStage.DiveModel.AValuesNitrogen[compartment] * diveStage.DiveModel.DiveModelProfile.NitrogenTissuePressures[compartment]) + (diveStage.DiveModel.AValuesHelium[compartment] * diveStage.DiveModel.DiveModelProfile.HeliumTissuePressures[compartment])) / diveStage.DiveModel.DiveModelProfile.TotalTissuePressures[compartment], 4);

    private float CalculateBValues(int compartment) => (float)Math.Round(((diveStage.DiveModel.BValuesNitrogen[compartment] * diveStage.DiveModel.DiveModelProfile.NitrogenTissuePressures[compartment]) + (diveStage.DiveModel.BValuesHelium[compartment] * diveStage.DiveModel.DiveModelProfile.HeliumTissuePressures[compartment])) / diveStage.DiveModel.DiveModelProfile.TotalTissuePressures[compartment], 4);
}
