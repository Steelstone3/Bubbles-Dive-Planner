using System;

namespace BubblesDivePlanner.Controllers.DiveBoundaries
{
    public static class MaximumOperatingDepthController
    {
        public static double CalculateMaximumOperatingDepth(double oxygenPercentage)
        {
            const double toleratedPartialPressure = 1.4;
            double oxygenPartialPressure = oxygenPercentage / 100;
            double toleratedPressure = toleratedPartialPressure / oxygenPartialPressure;
            return Math.Round(toleratedPressure * 10 - 10, 2);
        }
    }
}