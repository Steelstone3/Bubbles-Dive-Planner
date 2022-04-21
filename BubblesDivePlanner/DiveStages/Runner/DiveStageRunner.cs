using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;

namespace BubblesDivePlanner.DiveStages.Runner
{
    public class DiveStageRunner : IDiveStageRunner
    {
        public IResultModel RunDiveStages(IDiveModel diveModel, IDiveStepModel diveStepModel, ICylinderSetupModel selectedCylinder, IResultModel resultModel)
        {
            var diveStageCommandFactory = new DiveStageCommandFactory(diveModel, diveStepModel, selectedCylinder, resultModel);
            var stages = diveStageCommandFactory.CreateDiveStages();

            foreach (var stage in stages)
            {
                stage.RunDiveStage();
            }

            return resultModel;
        }
    }
}