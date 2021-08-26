using BubblesDivePlanner.Contracts.Models.Plan;

namespace BubblesDivePlanner.Models.Plan
{
    public class DiveStepModel : IDiveStepModel
    {
        public int Depth
        {
            get; set;
        }

        public int Time
        {
            get; set;
        }
    }
}