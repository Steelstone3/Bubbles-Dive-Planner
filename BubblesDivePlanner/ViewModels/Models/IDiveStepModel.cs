namespace BubblesDivePlanner.ViewModels.Models
{
    public interface IDiveStepModel
    {
        byte Depth { get; set; }
        byte Time { get; set; }
        bool ValidateDiveStep(IDiveStepModel diveStep);
        IDiveStepModel DeepClone();
    }
}