using System.Collections.Generic;
using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Models
{
    public class CnsToxicityModel : ICnsToxicityContract
    {
        public List<double> OxygenPartialPressureConstant
        {
            get; private set;
        } = new List<double>(new double[] { 1.6, 1.5, 1.4, 1.3, 1.2, 1.1, 1.0, 0.9, 0.8, 0.7, 0.6 });

        public List<int> MaximumSingleDiveDuration
        {
            get; private set;
        } = new List<int>(new int[] { 45, 120, 150, 180, 210, 240, 300, 360, 450, 570, 720 });

        public List<int> Total24HourDuration
        {
            get; private set;
        } = new List<int>(new int[] { 150, 180, 180, 210, 240, 270, 300, 360, 450, 570, 720 });
    }
}