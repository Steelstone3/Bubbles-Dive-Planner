using BubblesDivePlanner.ViewModels.DiveApplication.Plan;

namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan
{
    public interface IDivePlanSetupViewModel
    {
        GasMixtureSelectorViewModel GasMixture { get; set; }

        IDiveModelSelectorViewModel DiveModelSelector { get; set; }

        IDiveStepViewModel DiveStep { get; set; }

        IGasManagementViewModel GasManagement { get; set; }
    }
}