namespace BubblesDivePlanner.DiveStep
{
    public interface IDiveStepModel
    {
        byte Depth { get; set; }
        byte Time { get; set; }
        bool ValidateDiveStep(IDiveStepModel diveStep);
        IDiveStepModel DeepClone();
    }
}