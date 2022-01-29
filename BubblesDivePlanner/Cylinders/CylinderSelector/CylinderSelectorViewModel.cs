using System.Collections.ObjectModel;
using System.Reactive;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlannerTests.Cylinders.CylinderSetup;
using ReactiveUI;

namespace BubblesDivePlanner.Cylinders.CylinderSelector
{
    public class CylinderSelectorViewModel : ReactiveObject, ICylinderSelectorModel
    {
        public CylinderSelectorViewModel()
        {
            AddCylinderCommand = ReactiveCommand.Create(AddCylinder);
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

        private void AddCylinder()
        {
            Cylinders.Add(new CylinderPrototype().Clone(SelectedCylinder));
        }
    }
}