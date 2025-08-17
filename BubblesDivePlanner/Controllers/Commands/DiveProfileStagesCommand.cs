public class DiveProfileStagesCommand : IDiveProfileStagesCommand
{
    public void Run(DiveStage diveStage)
    {
        IDiveProfileStage[] diveProfileStages = new DiveProfileStagesFactory().Create(diveStage);

        foreach (IDiveProfileStage diveProfileStage in diveProfileStages)
        {
            diveProfileStage.Run();
        }
    }
}

public interface IDiveProfileStagesCommand
{
    void Run(DiveStage diveStage);
}