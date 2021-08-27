using System;
using System.Collections.ObjectModel;
using System.Reactive;
using BubblesDivePlanner.Contracts.Models.Plan;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.Controllers.Information;
using BubblesDivePlanner.Models.Plan;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DiveApplication.Plan
{
    public class GasMixtureSelectorViewModel : ViewModelBase, IGasMixtureSelectorViewModel
    {
        private MaxOperatingDepthController _maxOperatingDepthController;

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

        private IGasMixtureModel _selectedGasMixture = new GasMixtureModel();
        public IGasMixtureModel SelectedGasMixture
        {
            get => _selectedGasMixture;
            set
            {
                if (_selectedGasMixture != value)
                {
                    _selectedGasMixture = value;
                    UpdateMaximumOperatingDepth();
                    this.RaisePropertyChanged(nameof(SelectedGasMixture));
                }
            }
        }

        private IGasMixtureViewModel _newGasMixture = new GasMixtureViewModel();
        public IGasMixtureViewModel NewGasMixture
        {
            get => _newGasMixture;
            set
            {
                _newGasMixture = value;
            }
        }
        
        public bool ValidateGasMixture(IGasMixtureModel selectedGasMixture)
        {
            return selectedGasMixture != null;
        }

        public IObservable<bool> CanAddGasMixture
        {
            get => this.WhenAnyValue(vm => vm.NewGasMixture.GasName,
            vm => vm.NewGasMixture.Oxygen,
            vm => vm.NewGasMixture.Helium,
            (gasName, oxygen, helium) =>
            !string.IsNullOrEmpty(gasName)
            && oxygen >= 5.0
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
            GasMixtures.Add(NewGasMixture.Clone());
        }

        private void SetDefaults()
        {
            GasMixtureModel defaultGasMixture = new GasMixtureModel();
            SelectedGasMixture = defaultGasMixture;

            GasMixtures.Add(defaultGasMixture);
        }

        private double CalculateMaximumOperatingDepth() => _maxOperatingDepthController.CalculateMaximumOperatingDepth(_selectedGasMixture.Oxygen);
        private void UpdateMaximumOperatingDepth()
        {
            if (_selectedGasMixture == null)
            {
                MaximumOperatingDepth = 0;
            }
            else
            {
                MaximumOperatingDepth = CalculateMaximumOperatingDepth();
            }
        }
    }
}
