using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Models
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