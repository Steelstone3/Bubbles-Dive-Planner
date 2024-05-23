public class DiveProfileStagesFactory : IDiveProfileStagesFactory
{
    public void Run(IDiveStage diveStage)
    {
        IDiveProfileStage[] diveProfileStages = Create(diveStage);

        foreach (IDiveProfileStage diveProfileStage in diveProfileStages)
        {
            diveProfileStage.Run();
        }
    }

    private IDiveProfileStage[] Create(IDiveStage diveStage)
    {
        return [
            new AmbientPressures(diveStage),
            new TissuePressures(diveStage),
            new AbValues(diveStage),
            new ToleratedAmbientPressures(diveStage),
            new MaximumSurfacePressures(diveStage),
            new CompartmentLoads(diveStage),
        ];
    }
}

public interface IDiveProfileStagesFactory
{
    void Run(IDiveStage diveStage);
}