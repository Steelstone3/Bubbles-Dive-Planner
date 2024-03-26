// TODO AH Test
public class TissuePressures : IDiveProfileStage
{
    private readonly IDiveModel diveModel;
    private readonly IDiveStep diveStep;

    public TissuePressures(IDiveModel diveModel, IDiveStep diveStep)
    {
        this.diveModel = diveModel;
        this.diveStep = diveStep;
    }

    public void Run()
    {
        for (int compartment = 0; compartment < diveModel.CompartmentCount; compartment++)
        {
            CalculateTissuePressureNitrogen(compartment);
            CalculateTissuePressureHelium(compartment);
            CalculateTotalTissuesPressure(compartment);
        }
    }

    private void CalculateTissuePressureNitrogen(int compartment)
    {
        diveModel.DiveModelProfile.NitrogenTissuePressures[compartment] = (float)Math.Round(diveModel.DiveModelProfile.NitrogenTissuePressures[compartment] +
                                                                    ((diveModel.DiveModelProfile.NitrogenAtPressure -
                                                                    diveModel.DiveModelProfile.NitrogenTissuePressures[compartment]) *
                                                                    (1.0f - Math.Pow(2.0f, -(diveStep.Time / diveModel.NitrogenHalfTime[compartment])))), 4);
    }

    private void CalculateTissuePressureHelium(int compartment)
    {
        diveModel.DiveModelProfile.HeliumTissuePressures[compartment] = (float)Math.Round(diveModel.DiveModelProfile.HeliumTissuePressures[compartment] +
                                                                    ((diveModel.DiveModelProfile.HeliumAtPressure -
                                                                    diveModel.DiveModelProfile.HeliumTissuePressures[compartment]) *
                                                                    (1.0f - Math.Pow(2.0f, -(diveStep.Time / diveModel.HeliumHalfTime[compartment])))), 4);
    }

    private void CalculateTotalTissuesPressure(int compartment)
    {
        diveModel.DiveModelProfile.TotalTissuePressures[compartment] = diveModel.DiveModelProfile.HeliumTissuePressures[compartment] +
                                                                   diveModel.DiveModelProfile.NitrogenTissuePressures[compartment];
    }
}