using System.Collections.ObjectModel;
using BubblesDivePlanner.GasManagement.Cylinder;
using BubblesDivePlanner.GasManagement.GasMixture;
using ReactiveUI;

namespace BubblesDivePlanner.GasManagement
{
    public class GasManagementViewModel : ReactiveObject, IGasManagementModel
    {
        public ObservableCollection<ICylinderModel> Cylinders
        {
            get;
        } = new ObservableCollection<ICylinderModel>();

        private ICylinderModel _selectedCylinder = new CylinderViewModel();
        public ICylinderModel SelectedCylinder
        {
            get => _selectedCylinder;
            set => this.RaiseAndSetIfChanged(ref _selectedCylinder, value);
        }
    }
}