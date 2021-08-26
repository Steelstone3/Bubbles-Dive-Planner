using BubblesDivePlanner.Contracts.Models.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DiveApplication.Plan
{
    public class GasMixtureViewModel : ViewModelBase, IGasMixtureModel
    {
        public GasMixtureViewModel()
        {
            GasName = "Air";
            Oxygen = 21;
            Helium = 0;
        }

        private string _gasName;
        public string GasName
        {
            get => _gasName;
            set
            {
                _gasName = value;
                this.RaisePropertyChanged(nameof(GasName));
            }
        }

        private double _oxygen;
        public double Oxygen
        {
            get => _oxygen;
            set
            {
                _oxygen = value;
                Nitrogen = CalculateNitrogen();
                this.RaisePropertyChanged(nameof(Oxygen));
            }
        }

        private double _helium;
        public double Helium
        {
            get => _helium;
            set
            {
                _helium = value;
                Nitrogen = CalculateNitrogen();
                this.RaisePropertyChanged(nameof(Helium));
            }
        }

        private double _nitrogen;
        public double Nitrogen
        {
            get => _nitrogen;
            private set => this.RaiseAndSetIfChanged(ref _nitrogen, value);
        }

        private double CalculateNitrogen() => 100 - Oxygen - Helium;

        public IGasMixtureModel Clone()
        {
            return new GasMixtureViewModel()
            {
                GasName = this.GasName,
                Oxygen = this.Oxygen,
                Helium = this.Helium,
                Nitrogen = this.Nitrogen,
            };
        }
    }
}
