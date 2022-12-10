using System.Collections.ObjectModel;
using BubblesDivePlanner.Models.Cylinders;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DivePlan
{
    public class CylinderViewModel : ReactiveObject
    {
        public ObservableCollection<ICylinder> Cylinders
        {
            get;
        } = new ObservableCollection<ICylinder>();

        private ICylinder selectedCylinder;
        public ICylinder SelectedCylinder
        {
            get => selectedCylinder;
            set => this.RaiseAndSetIfChanged(ref selectedCylinder, value);
        }
    }
}