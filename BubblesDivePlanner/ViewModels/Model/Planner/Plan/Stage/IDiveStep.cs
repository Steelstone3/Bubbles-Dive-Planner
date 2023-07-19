namespace BubblesDivePlanner.ViewModels.Model.Planner.Plan.Stage
{
    public interface IDiveStep
    {
        byte Depth { get; set; }
        byte Time { get; set; }
    }
}