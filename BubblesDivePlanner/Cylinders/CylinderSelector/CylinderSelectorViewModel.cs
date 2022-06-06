using System;
using System.Collections.ObjectModel;
using System.Reactive;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using ReactiveUI;

namespace BubblesDivePlanner.Cylinders.CylinderSelector
{
    public class CylinderSelectorViewModel : ReactiveObject, ICylinderSelectorModel
    {
        public CylinderSelectorViewModel()
        {
            AddCylinderCommand = ReactiveCommand.Create(AddCylinder, CanAddCylinder);
        }

        public ObservableCollection<ICylinderSetupModel> Cylinders
        {
            get;
        } = new ObservableCollection<ICylinderSetupModel>();

        private ICylinderSetupModel _selectedCylinder = new CylinderSetupViewModel();
        public ICylinderSetupModel SelectedCylinder
        {
            get => _selectedCylinder;
            set => this.RaiseAndSetIfChanged(ref _selectedCylinder, value);
        }

        public ReactiveCommand<Unit, Unit> AddCylinderCommand { get; }

        public IObservable<bool> CanAddCylinder
        {
            get => this.WhenAnyValue(vm => vm.SelectedCylinder.CylinderName,
                vm => vm.SelectedCylinder.CylinderVolume,
                vm => vm.SelectedCylinder.CylinderPressure,
                vm => vm.SelectedCylinder.GasMixture.Oxygen,
                vm => vm.SelectedCylinder.GasMixture.Helium,
                vm => vm.SelectedCylinder.GasUsage.SurfaceAirConsumptionRate,

                (cylinderName, cylinderVolume, cylinderPressure, oxygen, helium, surfaceAirConsumptionRate) =>
                    ValidateCylinderSetup(cylinderName, cylinderVolume, cylinderPressure, oxygen, helium, surfaceAirConsumptionRate));
        }

        public bool ValidateSelectedCylinder(ICylinderSetupModel selectedCylinder)
        {
            return ValidateCylinderSetup(selectedCylinder.CylinderName, selectedCylinder.CylinderVolume, selectedCylinder.CylinderPressure, selectedCylinder.GasMixture.Oxygen, selectedCylinder.GasMixture.Helium, selectedCylinder.GasUsage.SurfaceAirConsumptionRate);
        }

        private bool ValidateCylinderSetup(string cylinderName, int cylinderVolume, int cylinderPressure, double oxygen, double helium, int surfaceAirConsumptionRate)
        {
            return !string.IsNullOrWhiteSpace(cylinderName) && cylinderVolume <= 30 && cylinderVolume >= 3 && cylinderPressure <= 300 && cylinderPressure >= 150 && oxygen <= 100 - helium && oxygen >= 5 && helium <= 100 - oxygen && helium >= 0 && surfaceAirConsumptionRate <= 30 && surfaceAirConsumptionRate >= 5;
        }

        private void AddCylinder()
        {
            Cylinders.Add(new CylinderPrototype().DeepClone(SelectedCylinder));
        }
    }
}