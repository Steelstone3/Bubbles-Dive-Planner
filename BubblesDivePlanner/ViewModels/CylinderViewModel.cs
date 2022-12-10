using BubblesDivePlanner.Models.Cylinders;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class CylinderViewModel : ReactiveObject
    {
        private ICylinder selectedCylinder = new Cylinder("", 0, 0, 0, new GasMixture(0, 0), 0, 0);
        public ICylinder SelectedCylinder
        {
            get => selectedCylinder;
            set => this.RaiseAndSetIfChanged(ref selectedCylinder, value);
        }
    }
}