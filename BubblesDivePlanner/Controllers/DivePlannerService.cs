using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Controllers.Cylinders;
using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.Controllers
{
    public static class DivePlannerService
    {
        public static void CalculateDiveStep(MainWindowViewModel vm)
        {
            VisibilityController.UpdateVisibilty(vm);
            new DiveStageRunner().RunDiveStages(vm.DivePlan.DiveModelSelector.SelectedDiveModel, vm.DivePlan.DiveStep, vm.DivePlan.CylinderSelector.SelectedCylinder);
            CalculateGasUsage(vm);
            AssignResults(vm);
            RecalculateDecompressionSteps(vm);
        }

        public static void RecalculateDecompressionSteps(MainWindowViewModel vm)
        {
            vm.DiveInformation.DecompressionProfile.DecompressionDiveSteps.Clear();
            IDiveStepModel[] diveSteps = DecompressionProfileController.CollateDecompressionDiveSteps(vm.DivePlan.DiveModelSelector.SelectedDiveModel.DeepClone(), vm.DivePlan.CylinderSelector.SelectedCylinder).ToArray();

            foreach (var diveStep in diveSteps)
            {
                vm.DiveInformation.DecompressionProfile.DecompressionDiveSteps.Add(diveStep);
            }
        }

        public static void CalculateDecompressionProfile(MainWindowViewModel vm)
        {
            if (vm.DiveInformation.DecompressionProfile.DecompressionDiveSteps.Count > 0)
            {
                foreach (var diveStep in vm.DiveInformation.DecompressionProfile.DecompressionDiveSteps)
                {
                    vm.DivePlan.DiveStep = diveStep;
                    new DiveStageRunner().RunDiveStages(vm.DivePlan.DiveModelSelector.SelectedDiveModel, vm.DivePlan.DiveStep, vm.DivePlan.CylinderSelector.SelectedCylinder);
                    CalculateGasUsage(vm);
                    AssignResults(vm);
                }

                vm.DiveInformation.DecompressionProfile.DecompressionDiveSteps.Clear();
            }
        }

        private static void CalculateGasUsage(MainWindowViewModel vm)
        {
            vm.DivePlan.CylinderSelector.SelectedCylinder.GasUsage.GasUsed = new GasUsageController().CalculateGasUsed(vm.DivePlan.DiveStep, vm.DivePlan.CylinderSelector.SelectedCylinder.GasUsage.SurfaceAirConsumptionRate);
            vm.DivePlan.CylinderSelector.SelectedCylinder.GasUsage.UpdateGasRemaining();
        }

        private static void AssignResults(MainWindowViewModel vm)
        {
            vm.ResultsOverview.LatestResult.DiveProfile = vm.DivePlan.DiveModelSelector.SelectedDiveModel.DiveProfile.DeepClone();
            vm.ResultsOverview.LatestResult.DiveStep = vm.DivePlan.DiveStep.DeepClone();

            vm.ResultsOverview.LatestResult.SelectedCylinder = new CylinderPrototype().DeepClone(vm.DivePlan.CylinderSelector.SelectedCylinder);
        }
    }
}