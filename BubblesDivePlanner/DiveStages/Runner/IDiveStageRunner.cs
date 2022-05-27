using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;

namespace BubblesDivePlanner.DiveStages.Runner
{
    public interface IDiveStageRunner
    {
        //RunDecompressionDiveStages() //Runs a iterative calculation algorithm that effiently gets a user out of decompression
        //Has an additional setup for decompression step size

        //SimulateDecompression() //Returns a collection of dive steps with a deep clone of the dive profile (using a fresh cloned dive profile each time)
        void RunDiveStages(IDiveModel diveModel, IDiveStepModel diveStepModel, ICylinderSetupModel selectedCylinder);
    }
}