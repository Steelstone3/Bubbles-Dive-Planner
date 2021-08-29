using System.Collections.Generic;
using BubblesDivePlanner.Contracts.Models.Information;

namespace BubblesDivePlanner.Models.Information
{
    public class CnsToxicityModel : ICnsToxicityModel
    {
        public List<double> OxygenPartialPressureConstant
        {
            get; private set;
        } = new(new double[] { 1.6, 1.5, 1.4, 1.3, 1.2, 1.1, 1.0, 0.9, 0.8, 0.7, 0.6 });

        public List<int> MaximumSingleDiveDuration
        {
            get; private set;
        } = new(new int[] { 45, 120, 150, 180, 210, 240, 300, 360, 450, 570, 720 });

        public List<int> Total24HourDuration
        {
            get; private set;
        } = new(new int[] { 150, 180, 180, 210, 240, 270, 300, 360, 450, 570, 720 });
    }
}