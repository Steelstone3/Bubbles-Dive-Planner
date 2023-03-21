using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.Controllers.Interfaces
{
    public interface IDiveStageRunner
    {
        void RunDiveStages(IDiveModel diveModel, IDiveStepModel diveStepModel, ICylinderSetupModel selectedCylinder);
    }
}