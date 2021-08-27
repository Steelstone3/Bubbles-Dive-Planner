namespace BubblesDivePlanner.Contracts.Models.Plan
{
    public interface IDiveStepModel
    {
        int Depth { get; set; }

        int Time { get; set; }
    }
}