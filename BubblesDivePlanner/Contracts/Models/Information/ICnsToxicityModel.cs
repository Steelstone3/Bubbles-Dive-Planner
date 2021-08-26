using System.Collections.Generic;

namespace BubblesDivePlanner.Contracts.Models.Information
{
    public interface ICnsToxicityModel
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