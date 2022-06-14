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
            new DiveStageRunner().RunDiveStages(vm.DiveModelSelector.SelectedDiveModel, vm.DiveStep, vm.CylinderSelector.SelectedCylinder);
            CalculateGasUsage(vm);
            AssignResults(vm);
            RecalculateDecompressionSteps(vm);
        }

        public void RecalculateDecompressionSteps(MainWindowViewModel vm)
        {
            vm.DiveInformation.DecompressionProfile.DecompressionDiveSteps.Clear();
            var diveSteps = new DecompressionProfileController().CollateDecompressionDiveSteps(vm.DiveModelSelector.SelectedDiveModel.DeepClone(), vm.CylinderSelector.SelectedCylinder).ToArray();

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
                    vm.DiveStep = diveStep;
                    new DiveStageRunner().RunDiveStages(vm.DiveModelSelector.SelectedDiveModel, vm.DiveStep, vm.CylinderSelector.SelectedCylinder);
                    CalculateGasUsage(vm);
                    AssignResults(vm);
                }

                vm.DiveInformation.DecompressionProfile.DecompressionDiveSteps.Clear();
            }
        }

        private void CalculateGasUsage(MainWindowViewModel vm)
        {
            vm.CylinderSelector.SelectedCylinder.GasUsage.GasUsed = new GasUsageController().CalculateGasUsed(vm.DiveStep, vm.CylinderSelector.SelectedCylinder.GasUsage.SurfaceAirConsumptionRate);
            vm.CylinderSelector.SelectedCylinder.GasUsage.UpdateGasRemaining();
        }

        private void AssignResults(MainWindowViewModel vm)
        {
            vm.ResultsOverviewModel.LatestResult.DiveProfileModel = vm.DiveModelSelector.SelectedDiveModel.DiveProfile.DeepClone();
            vm.ResultsOverviewModel.LatestResult.DiveStepModel = vm.DiveStep.DeepClone();

            vm.ResultsOverviewModel.LatestResult.CylinderSetupModel = new CylinderPrototype().DeepClone(vm.CylinderSelector.SelectedCylinder);
        }
    }
}