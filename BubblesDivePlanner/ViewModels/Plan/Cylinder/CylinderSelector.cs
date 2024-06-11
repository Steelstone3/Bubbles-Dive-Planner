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

    private ICylinder selectedCylinder = new Cylinder(new CylinderValidator(), new CylinderController());
    public ICylinder SelectedCylinder
    {
        get => selectedCylinder;
        set => this.RaiseAndSetIfChanged(ref selectedCylinder, value);
    }

    private void AddCylinder()
    {
        if (!SelectedCylinder.IsValid)
        {
            return;
        }

        // TODO AH need a deep clone here
        Cylinders.Add(SelectedCylinder);
    }
}

public interface ICylinderSelector
{
    ObservableCollection<ICylinder> Cylinders { get; }
    ICylinder SelectedCylinder { get; set; }
}