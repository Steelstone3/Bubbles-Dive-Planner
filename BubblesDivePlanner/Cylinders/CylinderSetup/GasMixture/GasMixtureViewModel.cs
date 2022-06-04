using System.Collections.Generic;
using ReactiveUI;

namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture
{
    public class GasMixtureViewModel : ReactiveObject, IGasMixtureModel
    {
        public GasMixtureViewModel()
        {
            Nitrogen = new GasMixtureController().CalculateNitrogenMixture(Oxygen, Helium);
        }

        private double _oxygen;
        public double Oxygen
        {
            get => _oxygen;

            set
            {
                this.RaiseAndSetIfChanged(ref _oxygen, value);
                Nitrogen = new GasMixtureController().CalculateNitrogenMixture(Oxygen, Helium);
            }
        }

        private double _helium;
        public double Helium
        {
            get => _helium;

            set
            {
                this.RaiseAndSetIfChanged(ref _helium, value);
                Nitrogen = new GasMixtureController().CalculateNitrogenMixture(Oxygen, Helium);
            }
        }

        private double _nitrogen;
        public double Nitrogen
        {
            get => _nitrogen;
            private set => this.RaiseAndSetIfChanged(ref _nitrogen, value);
        }

        public double MaximumOperatingDepth => Oxygen != 0 ? new MaximumOperatingDepthController().CalculateMaximumOperatingDepth(Oxygen) : 0;

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }
    }
}