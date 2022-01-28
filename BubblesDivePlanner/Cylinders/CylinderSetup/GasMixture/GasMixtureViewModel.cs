using ReactiveUI;

namespace BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture
{
    public class GasMixtureViewModel : ReactiveObject, IGasMixtureModel
    {
        public GasMixtureViewModel()
        {
            Nitrogen = new GasMixtureController().CalculateNitrogenMixture(Oxygen, Helium);
        }

        private int _oxygen;
        public int Oxygen
        {
            get => _oxygen;

            set
            {
                this.RaiseAndSetIfChanged(ref _oxygen, value);
                Nitrogen = new GasMixtureController().CalculateNitrogenMixture(Oxygen, Helium);
            }
        }

        private int _helium;
        public int Helium
        {
            get => _helium;

            set
            {
                this.RaiseAndSetIfChanged(ref _helium, value);
                Nitrogen = new GasMixtureController().CalculateNitrogenMixture(Oxygen, Helium);
            }
        }

        private int _nitrogen;
        public int Nitrogen
        {
            get => _nitrogen;
            private set => this.RaiseAndSetIfChanged(ref _nitrogen, value);
        }
    }
}