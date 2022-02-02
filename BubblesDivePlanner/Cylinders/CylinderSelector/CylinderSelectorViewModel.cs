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

        //TODO AH Check that cylinder is valid may need another cylinder for the one being created?
        // public IObservable<bool> CanAddCylinder
        // {
        //     TODO AH change the view models here
        //     get => this.WhenAnyValue(vm => vm.DiveModelSelector.SelectedDiveModel,
        //         vm => vm.CylinderSelector.SelectedCylinder,
        //         vm => vm.DiveStep.Depth,
        //         vm => vm.DiveStep.Time,
        //         (selectorDiveModel, selectorCylinder, depth, time) =>
        //             DiveModelSelector.ValidateSelectedDiveModel(selectorDiveModel)
        //             && CylinderSelector.ValidateSelectedCylinder(selectorCylinder)
        //             && DiveStep.ValidateDiveStep(depth, time));
        // }

        private void AddCylinder()
        {
            Cylinders.Add(new CylinderPrototype().Clone(SelectedCylinder));
        }
    }
}