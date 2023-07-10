namespace BubblesDivePlanner.ViewModels.Model.Plan
{
    public interface IDiveStep
    {
        byte Depth { get; set; }
        byte Time { get; set; }
    }
}