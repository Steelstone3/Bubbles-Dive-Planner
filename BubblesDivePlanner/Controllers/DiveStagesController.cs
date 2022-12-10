using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlanner.DiveStages;
using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers
{
    public class DiveStagesController : IDiveStagesController
    {
        public IDivePlan Run(IDivePlan divePlan)
        {
            var diveStageCommands = CreateDiveStageCommands(divePlan);

            foreach (var diveStage in diveStageCommands)
            {
                diveStage.RunDiveStage();
            }

            return divePlan;
        }

        private static IDiveStageCommand[] CreateDiveStageCommands(IDivePlan divePlan)
        {
            var diveModel = divePlan.DiveModel;
            var diveProfile = divePlan.DiveModel.DiveProfile;
            var diveStep = divePlan.DiveStep;
            var selectedCylinder = divePlan.SelectedCylinder;
            var gasMixture = selectedCylinder.GasMixture;

            return new IDiveStageCommand[]
            {
                new AmbientPressure(diveProfile, gasMixture, diveStep),
                new TissuePressure(diveModel, diveStep),
                new AbValues(diveModel),
                new ToleratedAmbientPressure(diveModel),
                new MaximumSurfacePressure(diveModel),
                new CompartmentLoad(diveModel),
                new DiveCeiling(diveProfile),
                new GasManagement(selectedCylinder, diveStep)
            };
        }
    }
}