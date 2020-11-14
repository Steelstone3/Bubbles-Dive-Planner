using System;
using System.Collections.ObjectModel;
using System.Reactive;
using DivePlannerMk3.Controllers.ModelConverters;
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
            && oxygen > 0.0
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
            //Add to gas mixtures list
            var gasMixtureModelConverter = new GasMixtureModelConverter();
            GasMixtures.Add(gasMixtureModelConverter.ConvertToModel(NewGasMixture));
        }

        private void SetDefaults()
        {
            GasMixtureModel defaultGasMixture = new GasMixtureModel();
            SelectedGasMixture = defaultGasMixture;

            GasMixtures.Add(defaultGasMixture);
        }
    }
}
