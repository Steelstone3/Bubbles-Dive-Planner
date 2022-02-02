namespace BubblesDivePlanner.DiveStep
{
    public interface IDiveStepModel
    {
        int Depth { get; set; }
        int Time { get; set; }
        bool ValidateDiveStep(IDiveStepModel diveStep);
    }
}