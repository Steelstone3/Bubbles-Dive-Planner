using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using BubblesDivePlanner.DecompressionProfile;
using BubblesDivePlanner.DiveStages.Runner;
using BubblesDivePlanner.Visibility;

namespace BubblesDivePlanner.ApplicationEntry
{
    public class DivePlannerService
    {
        public void CalculateDiveStep(MainWindowViewModel vm)
        {
            new VisibilityController().UpdateVisibilty(vm);
            new DiveStageRunner().RunDiveStages(vm.DivePlanner.DiveModelSelector.SelectedDiveModel, vm.DivePlanner.DiveStep, vm.DivePlanner.CylinderSelector.SelectedCylinder);
            CalculateGasUsage(vm);
            AssignResults(vm);
            RecalculateDecompressionSteps(vm);
        }

        public void RecalculateDecompressionSteps(MainWindowViewModel vm)
        {
            vm.DiveInformation.DecompressionProfile.DecompressionDiveSteps.Clear();
            var diveSteps = new DecompressionProfileController().CollateDecompressionDiveSteps(vm.DivePlanner.DiveModelSelector.SelectedDiveModel.DeepClone(), vm.DivePlanner.CylinderSelector.SelectedCylinder).ToArray();

            foreach (var diveStep in diveSteps)
            {
                vm.DiveInformation.DecompressionProfile.DecompressionDiveSteps.Add(diveStep);
            }
        }

        public void CalculateDecompressionProfile(MainWindowViewModel vm)
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

        private void CalculateGasUsage(MainWindowViewModel vm)
        {
            vm.DivePlanner.CylinderSelector.SelectedCylinder.GasUsage.GasUsed = new GasUsageController().CalculateGasUsed(vm.DivePlanner.DiveStep, vm.DivePlanner.CylinderSelector.SelectedCylinder.GasUsage.SurfaceAirConsumptionRate);
            vm.DivePlanner.CylinderSelector.SelectedCylinder.GasUsage.UpdateGasRemaining();
        }

        private void AssignResults(MainWindowViewModel vm)
        {
            vm.ResultsOverview.LatestResult.DiveProfileModel = vm.DivePlanner.DiveModelSelector.SelectedDiveModel.DiveProfile.DeepClone();
            vm.ResultsOverview.LatestResult.DiveStepModel = vm.DivePlanner.DiveStep.DeepClone();

            vm.ResultsOverview.LatestResult.CylinderSetupModel = new CylinderPrototype().DeepClone(vm.DivePlanner.CylinderSelector.SelectedCylinder);
        }
    }
}