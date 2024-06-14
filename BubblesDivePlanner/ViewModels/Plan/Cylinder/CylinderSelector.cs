using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;

public class CylinderSelector : ReactiveObject, ICylinderSelector
{
    public CylinderSelector()
    {
        AddCylinderCommand = ReactiveCommand.Create(AddCylinder); //, CanAddCylinder);
    }

    public ReactiveCommand<Unit, Unit> AddCylinderCommand { get; }

    public ObservableCollection<ICylinder> Cylinders
    {
        get;
    } = new ObservableCollection<ICylinder>();

    private ICylinder setupCylinder = new Cylinder(new CylinderValidator(), new CylinderController());
    public ICylinder SetupCylinder
    {
        get => setupCylinder;
        set => this.RaiseAndSetIfChanged(ref setupCylinder, value);
    }

    private ICylinder selectedCylinder;
    public ICylinder SelectedCylinder
    {
        get => selectedCylinder;
        set => this.RaiseAndSetIfChanged(ref selectedCylinder, value);
    }

    private void AddCylinder()
    {
        if (!SetupCylinder.IsValid)
        {
            return;
        }

        ICylinderPrototype cylinderPrototype = new CylinderPrototype();
        ICylinder clonedSelectedCylinder = cylinderPrototype.DeepClone(SetupCylinder);
        Cylinders.Add(clonedSelectedCylinder);
    }
}

public interface ICylinderSelector
{
    ObservableCollection<ICylinder> Cylinders { get; }
    ICylinder SetupCylinder { get; set; }
    ICylinder SelectedCylinder { get; set; }
}