using System;

namespace DivePlannerMk3.Controllers
{
    public class MaxOperatingDepthController
    {
        public double CalculateMaximumOperatingDepth(double oxygenPercentage)
        {
            const double toleratedPartialPressure = 1.4;

            double oxygenPartialPressure = ConvertOxygenToPartialPressure(oxygenPercentage);

            double toleratedPressure = CalculateMaximumOperatingPressure(toleratedPartialPressure, oxygenPartialPressure);

            return Math.Round(ConvertPressureToDepth(toleratedPressure), 2);
        }

        private double ConvertOxygenToPartialPressure(double oxygenPercentage) => oxygenPercentage / 100;
        private double CalculateMaximumOperatingPressure(double toleratedPartialPressure, double oxygenPartialPressure) => toleratedPartialPressure / oxygenPartialPressure;
        private double ConvertPressureToDepth(double toleratedPressure) => (toleratedPressure * 10) - 10;
    }
}