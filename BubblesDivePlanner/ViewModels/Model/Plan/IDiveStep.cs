namespace BubblesDivePlanner.ViewModels.Models.Plans
{
    public interface IDiveStep
    {
        byte Depth { get; set; }
        byte Time { get; set; }
    }
}