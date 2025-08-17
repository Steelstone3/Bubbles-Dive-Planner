public class TissuePressures : IDiveProfileStage
{
    private DiveStage diveStage;

    public TissuePressures(DiveStage diveStage)
    {
        this.diveStage = diveStage;
    }

    public void Run()
    {
        float[] newNitrogenTissuePressures = new float[diveStage.DiveModel.CompartmentCount];
        float[] newHeliumTissuePressures = new float[diveStage.DiveModel.CompartmentCount];
        float[] newTotalTissuePressures = new float[diveStage.DiveModel.CompartmentCount];

        for (int compartment = 0; compartment < diveStage.DiveModel.CompartmentCount; compartment++)
        {
            newNitrogenTissuePressures[compartment] = CalculateNitrogenTissuePressure(compartment);
            newHeliumTissuePressures[compartment] = CalculateHeliumTissuePressure(compartment);
            newTotalTissuePressures[compartment] = CalculateTotalTissuesPressure(compartment, newNitrogenTissuePressures, newHeliumTissuePressures);
        }

        diveStage.DiveModel.DiveModelProfile.NitrogenTissuePressures = newNitrogenTissuePressures;
        diveStage.DiveModel.DiveModelProfile.HeliumTissuePressures = newHeliumTissuePressures;
        diveStage.DiveModel.DiveModelProfile.TotalTissuePressures = newTotalTissuePressures;
    }

    private float CalculateNitrogenTissuePressure(int compartment) => (float)Math.Round(diveStage.DiveModel.DiveModelProfile.NitrogenTissuePressures[compartment] +
                                                                    ((diveStage.DiveModel.DiveModelProfile.NitrogenAtPressure -
                                                                    diveStage.DiveModel.DiveModelProfile.NitrogenTissuePressures[compartment]) *
                                                                    (1.0f - Math.Pow(2.0f, -(diveStage.DiveStep.Time / diveStage.DiveModel.NitrogenHalfTime[compartment])))), 4);

    private float CalculateHeliumTissuePressure(int compartment) => (float)Math.Round(diveStage.DiveModel.DiveModelProfile.HeliumTissuePressures[compartment] +
                                                                    ((diveStage.DiveModel.DiveModelProfile.HeliumAtPressure -
                                                                    diveStage.DiveModel.DiveModelProfile.HeliumTissuePressures[compartment]) *
                                                                    (1.0f - Math.Pow(2.0f, -(diveStage.DiveStep.Time / diveStage.DiveModel.HeliumHalfTime[compartment])))), 4);

    private float CalculateTotalTissuesPressure(int compartment, float[] newNitrogenTissuePressures, float[] newHeliumTissuePressures) => newNitrogenTissuePressures[compartment] + newHeliumTissuePressures[compartment];
}