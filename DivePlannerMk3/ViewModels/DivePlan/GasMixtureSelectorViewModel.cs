using System;
using System.Collections.ObjectModel;
using System.Reactive;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.Models;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class GasMixtureSelectorViewModel : ViewModelBase
    {
        MaxOperatingDepthController _maxOperatingDepthController;

        public GasMixtureSelectorViewModel()
        {
            AddGasMixtureCommand = ReactiveCommand.Create(AddGasMixture, CanAddGasMixture);
            _maxOperatingDepthController = new MaxOperatingDepthController();
            SetDefaults();
        }

        private double _maximumOperatingDepth;
        public double MaximumOperatingDepth
        {
           get => _maximumOperatingDepth;
           set => this.RaiseAndSetIfChanged(ref _maximumOperatingDepth, value);
        }

        public ObservableCollection<IGasMixtureModel> GasMixtures
        {
            get;
        } = new ObservableCollection<IGasMixtureModel>();

        private IGasMixtureModel _selectedGasMixture = new GasMixtureViewModel();
        public IGasMixtureModel SelectedGasMixture
        {
            get => _selectedGasMixture;
            set
            {
                if (_selectedGasMixture != value)
                {
                    _selectedGasMixture = value;
                    MaximumOperatingDepth = UpdateMaximumOperatingDepth();
                    this.RaisePropertyChanged(nameof(SelectedGasMixture));
                }
            }
        }

        private GasMixtureViewModel _newGasMixture = new GasMixtureViewModel();
        public GasMixtureViewModel NewGasMixture
        {
            get => _newGasMixture;
            set
            {
                _newGasMixture = value;
            }
        }

        public IObservable<bool> CanAddGasMixture
        {
            get => this.WhenAnyValue(vm => vm.NewGasMixture.GasName,
            vm => vm.NewGasMixture.Oxygen,
            vm => vm.NewGasMixture.Helium,
            (gasName, oxygen, helium) =>
            !string.IsNullOrEmpty(gasName)
            && oxygen > 5.0
            && oxygen <= 100.0
            && helium >= 0.0
            && helium <= 100.0
            && helium + oxygen <= 100);
        }

        public ReactiveCommand<Unit, Unit> AddGasMixtureCommand
        {
            get;
        }

        private void AddGasMixture()
        {
            GasMixtures.Add(NewGasMixture);
        }

        private void SetDefaults()
        {
            GasMixtureModel defaultGasMixture = new GasMixtureModel();
            SelectedGasMixture = defaultGasMixture;

            GasMixtures.Add(defaultGasMixture);
        }

        private double UpdateMaximumOperatingDepth() => _maxOperatingDepthController.CalculateMaximumOperatingDepth(_selectedGasMixture.Oxygen);
    }
}
