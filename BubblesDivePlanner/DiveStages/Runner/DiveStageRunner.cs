using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;

namespace BubblesDivePlanner.DiveStages.Runner
{
    public class DiveStageRunner : IDiveStageRunner
    {
        public void RunDiveStages(IDiveModel diveModel, IDiveStepModel diveStepModel, ICylinderSetupModel selectedCylinder)
        {
            // var resultModel = new ResultViewModel();
            var diveStageCommandFactory = new DiveStageCommandFactory(diveModel, diveStepModel, selectedCylinder);
            var stages = diveStageCommandFactory.CreateDiveStages();

            foreach (var stage in stages)
            {
                stage.RunDiveStage();
            }
        }
    }
}