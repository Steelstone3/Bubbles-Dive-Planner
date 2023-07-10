namespace BubblesDivePlanner.ViewModels.Models.DivePlan
{
    public interface IDiveStep
    {
        byte Depth { get; set; }
        byte Time { get; set; }
    }
}