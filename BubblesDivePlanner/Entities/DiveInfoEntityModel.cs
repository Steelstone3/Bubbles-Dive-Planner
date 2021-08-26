using System.Collections.Generic;
using BubblesDivePlanner.Contracts.Entities;

namespace BubblesDivePlanner.Entities
{
    public class DiveInfoEntityModel : IEntityModel
    {
        public List<int> MaximumSingleDiveDuration { get; internal set; }
        public List<double> OxygenPartialPressureConstant { get; internal set; }
        public List<int> Total24HourDuration { get; internal set; }
        public double DiveCeiling { get; internal set; }
    }
}