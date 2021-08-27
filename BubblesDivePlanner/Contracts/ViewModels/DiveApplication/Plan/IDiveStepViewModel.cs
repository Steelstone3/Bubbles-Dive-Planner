using BubblesDivePlanner.Contracts.Models.Plan;

namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan
{
    public interface IDiveStepViewModel : IDiveStepModel
    {
        bool ValidateDiveStep(int depth, int time, double maximumOperatingDepth);
    }
}