using System;

namespace BubblesDivePlanner.Models.Cylinders
{
    public class GasMixture : IGasMixture
    {
        public GasMixture(byte oxygen, byte helium)
        {
            Oxygen = oxygen;
            CalculateMaximumOperatingDepth();
            Helium = helium;
            CalculateNitrogen();
        }

        public byte Oxygen { get; private set; }
        public byte Helium { get; private set; }
        public byte Nitrogen { get; private set; }
        public double MaximumOperatingDepth { get; private set; }

        private void CalculateNitrogen() => Nitrogen = (byte)(100 - Oxygen - Helium);

        private void CalculateMaximumOperatingDepth()
        {
            const double toleratedPartialPressure = 1.4;
            double oxygenPartialPressure = Oxygen != 0 ? (double)Oxygen / 100 : 0;
            double toleratedPressure = oxygenPartialPressure != 0 ? toleratedPartialPressure / oxygenPartialPressure : 0;
            MaximumOperatingDepth = toleratedPressure != 0 ? Math.Round(toleratedPressure * 10 - 10, 2) : 0;
        }
    }
}