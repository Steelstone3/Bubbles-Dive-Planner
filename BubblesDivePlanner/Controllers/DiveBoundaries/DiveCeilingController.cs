using System;
using System.Linq;

namespace BubblesDivePlanner.Controllers.DiveBoundaries
{
    public static class DiveCeilingController
    {
        public static double CalculateDiveCeiling(double[] toleratedAmbientPressures)
        {
            return Math.Round((toleratedAmbientPressures.Max() - 1.0) * 10.0, 2);
        }
    }
}