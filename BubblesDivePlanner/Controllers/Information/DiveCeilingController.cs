using System;
using System.Collections.Generic;
using System.Linq;

namespace BubblesDivePlanner.Controllers.Information
{
    public class DiveCeilingController
    {
        public double CalculateDiveCeiling(IEnumerable<double> toleratedAmbientPressures)
        {
            return Math.Round(( toleratedAmbientPressures.Max() - 1.0 ) * 10.0, 2);
        }
    }
}