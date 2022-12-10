using System.Collections.ObjectModel;
using BubblesDivePlanner.Models.Cylinders;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class CylinderViewModel : ReactiveObject
    {
        public ObservableCollection<ICylinder> Cylinders
        {
            get;
        } = new ObservableCollection<ICylinder>();

        private ICylinder selectedCylinder = new Cylinder("", 0, 0, 0, new GasMixture(0, 0), 0, 0);
        public ICylinder SelectedCylinder
        {
            get => selectedCylinder;
            set => this.RaiseAndSetIfChanged(ref selectedCylinder, value);
        }
    }
}