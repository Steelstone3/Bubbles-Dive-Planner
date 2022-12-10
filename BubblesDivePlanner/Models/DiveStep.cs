namespace BubblesDivePlanner.Models
{
    public class DiveStep : IDiveStep
    {
        public DiveStep(byte depth, byte time)
        {
            Depth = depth;
            Time = time;
        }

        public byte Depth { get; }
        public byte Time { get; }
    }
}