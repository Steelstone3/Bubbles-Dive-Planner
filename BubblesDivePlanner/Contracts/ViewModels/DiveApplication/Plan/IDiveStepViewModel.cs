namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan
{
    public interface IDiveStepViewModel
    {
        int Depth { get; set; }

        int Time { get; set; }

        bool ValidateDiveStep(int depth, int time, double maximumOperatingDepth);
    }
}