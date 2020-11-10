using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using DivePlannerMk3.Models;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class PlanGasMixtureViewModel : ViewModelBase
    {
        public PlanGasMixtureViewModel()
        {
            AddGasMixtureCommand = ReactiveCommand.Create(AddGasMixture, CanAddGasMixture);
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
            set
            {
                if (_selectedGasMixture != value)
                {
                    _selectedGasMixture = value;
                    this.RaisePropertyChanged(nameof(SelectedGasMixture));
                }
            }
        }

        private GasMixtureModel _newGasMixture = new GasMixtureModel();
        public GasMixtureModel NewGasMixture
        {
            get => _newGasMixture;
            set
            {
                _newGasMixture = value;
                this.RaisePropertyChanged(nameof(NewGasMixture));
            }
        }

        public ReactiveCommand<Unit, Unit> AddGasMixtureCommand
        {
            get;
        }

        public IObservable<bool> CanAddGasMixture
        {
            get => this.WhenAnyValue(vm => vm.NewGasMixture.GasName, (gasName) => !string.IsNullOrEmpty(gasName));
            
            //TODO only allow unique name enteries
            //&& GasMixtures.Any(gas => gas.GasName.Contains(gasName)));
        }

        private void AddGasMixture()
        {
            //Add to gas mixtures list
            var newGasMixture = new GasMixtureModel()
            {
                GasName = NewGasMixture.GasName,
                Oxygen = NewGasMixture.Oxygen,
                Helium = NewGasMixture.Helium,
                Nitrogen = CalculateNitrogen(NewGasMixture.Oxygen,NewGasMixture.Helium),
            };

            GasMixtures.Add(newGasMixture);
        }

        private double CalculateNitrogen(double oxygenPercentage, double heliumPercentage) => 100 - oxygenPercentage - heliumPercentage;

        private void SetDefaults()
        {
            GasMixtureModel defaultGasMixture = new GasMixtureModel();
            SelectedGasMixture = defaultGasMixture;

            const double ean32 = 32;
            const double ean36 = 32;
            const double ean50 = 32;

            GasMixtureModel nitrox32 = new GasMixtureModel()
            {
                GasName = "EAN32",
                Helium = 0,
                Oxygen = ean32,
                Nitrogen = CalculateNitrogen(ean32,0),
            };

            GasMixtureModel nitrox36 = new GasMixtureModel()
            {
                GasName = "EAN36",
                Helium = 0,
                Oxygen = ean36,
                Nitrogen = CalculateNitrogen(ean36,0),
            };

            GasMixtureModel nitrox50 = new GasMixtureModel()
            {
                GasName = "EAN50",
                Helium = 0,
                Oxygen = ean50,
                Nitrogen = CalculateNitrogen(ean50,0),
            };

            GasMixtures.Add(defaultGasMixture);
            GasMixtures.Add(nitrox32);
            GasMixtures.Add(nitrox36);
            GasMixtures.Add(nitrox50);
        }
    }
}
