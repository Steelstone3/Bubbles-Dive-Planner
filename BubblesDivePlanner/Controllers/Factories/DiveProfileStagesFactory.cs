public class DiveProfileStagesFactory : IDiveProfileStagesFactory
{
    public IDiveProfileStage[] Create(DiveStage diveStage)
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
    IDiveProfileStage[] Create(DiveStage diveStage);
}