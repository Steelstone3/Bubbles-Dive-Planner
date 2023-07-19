using BubblesDivePlanner.Commands;
using BubblesDivePlanner.Commands.Interfaces;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Stage;

namespace BubblesDivePlanner.Controllers
{
    public static class DiveController
    {
        public static void Run(IDiveStage diveStage)
        {
            foreach (IDiveStageCommand command in CreateDiveStageCommands(diveStage))
            {
                command.RunDiveStage();
            }
        }

        private static IDiveStageCommand[] CreateDiveStageCommands(IDiveStage diveStage)
        {
            IDiveModel diveModel = diveStage.DiveModel;
            IDiveProfile diveProfile = diveStage.DiveModel.DiveProfile;
            IDiveStep diveStep = diveStage.DiveStep;
            ICylinder selectedCylinder = diveStage.Cylinder;
            IGasMixture gasMixture = selectedCylinder.GasMixture;

            return new IDiveStageCommand[]
            {
                new AmbientPressureCommand(diveProfile, gasMixture, diveStep),
                new TissuePressureCommand(diveModel, diveStep),
                new ABValuesCommand(diveModel),
                new ToleratedAmbientPressureCommand(diveModel),
                new MaximumSurfacePressureCommand(diveModel),
                new CompartmentLoadCommand(diveModel),
                // new DiveCeilingCommand(diveProfile),
                // new GasManagementCommand(selectedCylinder, diveStep)
            };
        }
    }
}