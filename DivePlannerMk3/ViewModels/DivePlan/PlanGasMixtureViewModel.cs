using System;
using System.Collections.ObjectModel;
using System.Reactive;
using DivePlannerMk3.Models;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class PlanGasMixtureViewModel : ViewModelBase
    {
        public PlanGasMixtureViewModel()
        {
            AddGasMixtureCommand = ReactiveCommand.Create( AddGasMixture );

            SetDefaults();
        }

        public ObservableCollection<GasMixtureModel> GasMixtures
        {
            get;
        } = new ObservableCollection<GasMixtureModel>();

        private GasMixtureModel _selectedGasMixture;
        public GasMixtureModel SelectedGasMixture
        {
            get => _selectedGasMixture;
            set => this.RaiseAndSetIfChanged( ref _selectedGasMixture, value );
        }

        public ReactiveCommand<Unit, Unit> AddGasMixtureCommand
        {
            get;
        }

        private void AddGasMixture()
        {
            //TODO AH Add a gas mixture to the list box containing the gas mixtures from a pop-up containing the fields
        }

        private void SetDefaults()
        {
            GasMixtureModel defaultGasMixture = new GasMixtureModel();
            SelectedGasMixture = defaultGasMixture;

            GasMixtureModel nitrox32 = new GasMixtureModel()
            {
                GasName = "EAN32",
                Helium = 0,
                Oxygen = 32,
                Nitrogen = 100 - 32,
            };

            GasMixtureModel nitrox36 = new GasMixtureModel()
            {
                GasName = "EAN36",
                Helium = 0,
                Oxygen = 36,
                Nitrogen = 100 - 36,
            };

            GasMixtureModel nitrox50 = new GasMixtureModel()
            {
                GasName = "EAN50",
                Helium = 0,
                Oxygen = 50,
                Nitrogen = 100 - 50,
            };

            GasMixtures.Add( defaultGasMixture );
            GasMixtures.Add( nitrox32 );
            GasMixtures.Add( nitrox36 );
            GasMixtures.Add( nitrox50 );
        }
    }
}
