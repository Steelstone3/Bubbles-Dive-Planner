using BubblesDivePlanner.Commands.Interfaces;

namespace BubblesDivePlanner.Controllers.Interfaces
{
    public interface IDiveStageCommandFactory
    {
        IDiveStageCommand[] CreateDiveStages();

        //IDiveStageCommand[] CreateDecompressionDiveStages(); //With an added decompression dive stage that outputs the dive steps collection
    }
}