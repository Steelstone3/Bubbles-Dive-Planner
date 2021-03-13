using System.Collections.Generic;
using DivePlannerMk3.Contracts.DataAccessContracts;

namespace DivePlannerMk3.DataAccessLayer.EntityModels
{
    public class DiveInfoEntityModel : IEntityModel
    {
        public List<int> MaximumSingleDiveDuration { get; internal set; }
        public List<double> OxygenPartialPressureConstant { get; internal set; }
        public List<int> Total24HourDuration { get; internal set; }
        public double DiveCeiling { get; internal set; }
    }
}