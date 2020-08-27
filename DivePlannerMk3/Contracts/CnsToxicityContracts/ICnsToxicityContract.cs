using System.Collections.Generic;

namespace DivePlannerMk3.Contracts
{
    public interface ICnsToxicityContract
    {
        List<double> OxygenPartialPressureConstant
        {
            get;
        }

        List<int> MaximumSingleDiveDuration
        {
            get;
        }

        List<int> Total24HourDuration
        {
            get;
        }
    } 
}