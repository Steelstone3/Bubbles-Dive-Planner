namespace BubblesDivePlanner.DiveStages.Runner
{
    public interface IDiveStageCommandFactory
    {
        IDiveStageCommand[] CreateDiveStages();

        //IDiveStageCommand[] CreateDecompressionDiveStages(); //With an added decompression dive stage that outputs the dive steps collection
    }
}