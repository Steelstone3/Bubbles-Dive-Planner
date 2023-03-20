using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.Controllers.Interfaces
{
    public interface IDiveStageRunner
    {
        void RunDiveStages(IDiveModel diveModel, IDiveStepModel diveStepModel, ICylinderSetupModel selectedCylinder);
    }
}