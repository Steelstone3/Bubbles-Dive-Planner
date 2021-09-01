namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan
{
    public interface IDivePlanSetupViewModel
    {
        IGasMixtureSelectorViewModel GasMixture { get; set; }

        IDiveModelSelectorViewModel DiveModelSelector { get; set; }

        IDiveStepViewModel DiveStep { get; set; }

        IGasManagementViewModel GasManagement { get; set; }
    }
}