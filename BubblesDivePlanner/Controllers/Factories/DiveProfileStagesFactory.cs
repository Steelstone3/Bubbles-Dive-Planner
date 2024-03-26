public class DiveProfileStagesFactory : IDiveProfileStagesFactory
{
    public IDiveProfileStage[] Create(IDiveStage diveStage)
    {
        return new IDiveProfileStage[] {
            new AmbientPressures(diveStage.DiveModel.DiveModelProfile, diveStage.GasMixture, diveStage.DiveStep),
            new TissuePressures(diveStage.DiveModel, diveStage.DiveStep),
            new AbValues(diveStage.DiveModel),
            new ToleratedAmbientPressures(diveStage.DiveModel),
            new MaximumSurfacePressures(diveStage.DiveModel),
            new CompartmentLoads(diveStage.DiveModel),
        };
    }

    public void Run(IDiveStage diveStage)
    {
        foreach (IDiveProfileStage diveProfileStage in Create(diveStage))
        {
            diveProfileStage.Run();
        }
    }
}

public interface IDiveProfileStagesFactory
{
    IDiveProfileStage[] Create(IDiveStage diveStage);
    void Run(IDiveStage diveStage);

}