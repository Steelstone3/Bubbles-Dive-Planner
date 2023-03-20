using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using BubblesDivePlanner.DecompressionProfile;
using BubblesDivePlanner.DiveStages.Runner;
using BubblesDivePlanner.Visibility;

namespace BubblesDivePlanner.ApplicationEntry
{
    public static class DivePlannerService
    {
        public static void CalculateDiveStep(MainWindowViewModel vm)
        {
            VisibilityController.UpdateVisibilty(vm);
            new DiveStageRunner().RunDiveStages(vm.DivePlanner.DiveModelSelector.SelectedDiveModel, vm.DivePlanner.DiveStep, vm.DivePlanner.CylinderSelector.SelectedCylinder);
            CalculateGasUsage(vm);
            AssignResults(vm);
            RecalculateDecompressionSteps(vm);
        }

        public static void RecalculateDecompressionSteps(MainWindowViewModel vm)
        {
            vm.DiveInformation.DecompressionProfile.DecompressionDiveSteps.Clear();
            var diveSteps = DecompressionProfileController.CollateDecompressionDiveSteps(vm.DivePlanner.DiveModelSelector.SelectedDiveModel.DeepClone(), vm.DivePlanner.CylinderSelector.SelectedCylinder).ToArray();

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
                    vm.DivePlanner.DiveStep = diveStep;
                    new DiveStageRunner().RunDiveStages(vm.DivePlanner.DiveModelSelector.SelectedDiveModel, vm.DivePlanner.DiveStep, vm.DivePlanner.CylinderSelector.SelectedCylinder);
                    CalculateGasUsage(vm);
                    AssignResults(vm);
                }

                vm.DiveInformation.DecompressionProfile.DecompressionDiveSteps.Clear();
            }
        }

        private static void CalculateGasUsage(MainWindowViewModel vm)
        {
            vm.DivePlanner.CylinderSelector.SelectedCylinder.GasUsage.GasUsed = new GasUsageController().CalculateGasUsed(vm.DivePlanner.DiveStep, vm.DivePlanner.CylinderSelector.SelectedCylinder.GasUsage.SurfaceAirConsumptionRate);
            vm.DivePlanner.CylinderSelector.SelectedCylinder.GasUsage.UpdateGasRemaining();
        }

        private static void AssignResults(MainWindowViewModel vm)
        {
            vm.ResultsOverview.LatestResult.DiveProfile = vm.DivePlanner.DiveModelSelector.SelectedDiveModel.DiveProfile.DeepClone();
            vm.ResultsOverview.LatestResult.DiveStep = vm.DivePlanner.DiveStep.DeepClone();

            vm.ResultsOverview.LatestResult.SelectedCylinder = new CylinderPrototype().DeepClone(vm.DivePlanner.CylinderSelector.SelectedCylinder);
        }
    }
}