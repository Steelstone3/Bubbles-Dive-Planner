using System;
using BubblesDivePlanner.ViewModels.Models.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan
{
    public class GasMixture : ReactiveObject, IGasMixture
    {
        private float oxygen;
        public float Oxygen
        {
            get => oxygen;
            set => this.RaiseAndSetIfChanged(ref oxygen, value);
        }

        private float helium;
        public float Helium
        {
            get => helium;
            set => this.RaiseAndSetIfChanged(ref helium, value);
        }

        public float Nitrogen
        {
            get => 100 - Oxygen - Helium;
        }

        public float MaximumOperatingDepth
        {
            get => Oxygen == 0 ? 0 : CalculateMaximumOperatingDepth(Oxygen);
        }

        // TODO AH Move to a controller
        private static float CalculateMaximumOperatingDepth(double oxygenPercentage)
        {
            const double toleratedPartialPressure = 1.4;
            double oxygenPartialPressure = oxygenPercentage / 100;
            double toleratedPressure = toleratedPartialPressure / oxygenPartialPressure;
            return (float)Math.Round((toleratedPressure * 10) - 10, 2);
        }
    }
}