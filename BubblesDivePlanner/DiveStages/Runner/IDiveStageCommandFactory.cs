namespace BubblesDivePlanner.DiveStages.Runner
{
    public interface IDiveStageCommandFactory
    {
        IDiveStageCommand[] CreateDiveStages();
    }
}