using ReactiveUI;

namespace BubblesDivePlanner.GasManagement.GasMixture
{
    public class GasMixtureViewModel : ReactiveObject, IGasMixtureModel
    {
        private IGasMixtureController _gasMixtureController = new GasMixtureController();

        public GasMixtureViewModel(IGasMixtureController gasMixtureController)
        {
            _gasMixtureController = gasMixtureController;
        }

        private int _oxygen;
        public int Oxygen
        {
            get => _oxygen;

            set
            {
                this.RaiseAndSetIfChanged(ref _oxygen, value);
                _gasMixtureController.CalculateNitrogenMixture(Oxygen, Helium);
            }
        }

        private int _helium;
        public int Helium
        {
            get => _helium;

            set
            {
                this.RaiseAndSetIfChanged(ref _helium, value);
                _gasMixtureController.CalculateNitrogenMixture(Oxygen, Helium);
            }
        }

        public int Nitrogen => _gasMixtureController.CalculateNitrogenMixture(Oxygen, Helium);
    }
}