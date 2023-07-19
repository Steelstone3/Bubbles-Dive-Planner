using System.Collections.ObjectModel;
using System.Reactive;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan.Cylinders
{
    public class CylinderSelection : ViewModelBase, ICylinderSelectionVM
    {
        public CylinderSelection()
        {
            AddCylinderCommand = ReactiveCommand.Create(AddCylinder);
        }

        public ObservableCollection<ICylinder> Cylinders
        {
            get;
        } = new ObservableCollection<ICylinder>();

        private ICylinder cylinder = new Cylinder();
        public ICylinder Cylinder
        {
            get => cylinder;
            set => this.RaiseAndSetIfChanged(ref cylinder, value);
        }

        public ReactiveCommand<Unit, Unit> AddCylinderCommand { get; }

        public void AddCylinder()
        {
            Cylinders.Add(new Cylinder(Cylinder));
        }
    }
}