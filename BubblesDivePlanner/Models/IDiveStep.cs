namespace BubblesDivePlanner.Models
{
    public interface IDiveStep
    {
        byte Depth { get; }
        byte Time { get; }
    }
}