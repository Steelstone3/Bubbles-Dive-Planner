using System.Collections.ObjectModel;
using System.Reactive;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Plan.Cylinders
{
    public class CylinderSelection : ViewModelBase, ICylinderSelection
    {
        public CylinderSelection()
        {
            AddCylinderCommand = ReactiveCommand.Create(AddCylinder);
        }

        public ObservableCollection<ICylinder> Cylinders
        {
            get;
        } = new ObservableCollection<ICylinder>();

        private ICylinder selectedCylinder = new Cylinder();
        public ICylinder SelectedCylinder
        {
            get => selectedCylinder;
            set => this.RaiseAndSetIfChanged(ref selectedCylinder, value);
        }

        public ReactiveCommand<Unit, Unit> AddCylinderCommand { get; }

        public void AddCylinder()
        {
            Cylinders.Add(SelectedCylinder);
        }
    }
}