public class DiveStageClone
{
    public IDiveStage Clone(IDiveStage diveStage)
    {
        return new DiveStage()
        {
            DiveModel = new DiveModelClone().Clone(diveStage.DiveModel),
            DiveStep = new DiveStepClone().Clone(diveStage.DiveStep),
            GasMixture = new GasMixtureClone().Clone(diveStage.GasMixture)
        };
    }
}