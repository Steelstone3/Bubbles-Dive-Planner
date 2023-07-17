using System;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan.Cylinders
{
    public class GasMixture : ViewModelBase, IGasMixture
    {
        private float oxygen;
        public float Oxygen
        {
            get => oxygen;
            set
            {
                this.RaiseAndSetIfChanged(ref oxygen, value);
                MaximumOperatingDepth = CalculateMaximumOperatingDepth(Oxygen);
                Nitrogen = CalculateNitrogen();
            }
        }

        private float helium;
        public float Helium
        {
            get => helium;
            set
            {
                this.RaiseAndSetIfChanged(ref helium, value);
                Nitrogen = CalculateNitrogen();
            }
        }

        private float nitrogen = 100;
        public float Nitrogen
        {
            get => nitrogen;
            private set => this.RaiseAndSetIfChanged(ref nitrogen, value);
        }

        private float maximumOperatingDepth;
        public float MaximumOperatingDepth
        {
            get => maximumOperatingDepth;
            private set => this.RaiseAndSetIfChanged(ref maximumOperatingDepth, value);
        }

        // TODO AH Move to a controller
        private float CalculateNitrogen() => 100 - Oxygen - Helium;

        // TODO AH Move to a controller
        private static float CalculateMaximumOperatingDepth(double oxygenPercentage)
        {
            if (oxygenPercentage == 0)
            {
                return 0f;
            }

            const double toleratedPartialPressure = 1.4;
            double oxygenPartialPressure = oxygenPercentage / 100;
            double toleratedPressure = toleratedPartialPressure / oxygenPartialPressure;
            return (float)Math.Round(toleratedPressure * 10 - 10, 2);
        }
    }
}