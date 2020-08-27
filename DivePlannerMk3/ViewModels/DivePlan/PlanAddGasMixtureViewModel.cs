using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class PlanAddGasMixtureViewModel : ViewModelBase
    {
        //TODO AH Use as a pop-up for adding new gas mixtures on add gas mixutures button click in place of PlanGasMixtureViewModel containing the properties

        public string GasName
        {
            get; set;
        } = "Air";

        private double _oxygen = 21;
        public double Oxygen
        {
            get => _oxygen;
            set
            {
                this.RaiseAndSetIfChanged( ref _oxygen, value );
                this.RaisePropertyChanged( nameof( Nitrogen ) );
            }
        }

        private double _helium = 0;
        public double Helium
        {
            get => _helium;
            set
            {
                this.RaiseAndSetIfChanged( ref _helium, value );
                this.RaisePropertyChanged( nameof( Nitrogen ) );
            }
        }

        public double Nitrogen => 100 - ( _oxygen + _helium );
    }
}
