using System.Collections.ObjectModel;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using ReactiveUI;

namespace BubblesDivePlanner.Cylinders.CylinderSelector
{
    public class CylinderSelectorViewModel : ReactiveObject, ICylinderSelectorModel
    {
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
    }
}