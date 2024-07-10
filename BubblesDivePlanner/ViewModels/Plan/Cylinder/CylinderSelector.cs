using System.Collections.ObjectModel;
using System.Reactive;
using System.Text.Json.Serialization;
using ReactiveUI;

public class CylinderSelector : ReactiveObject, ICylinderSelector
{
    public CylinderSelector()
    {
        AddCylinderCommand = ReactiveCommand.Create(AddCylinder); //, CanAddCylinder);
    }

    [JsonIgnore]
    public Action SelectedCylinderChanged { get; set; }

    [JsonIgnore]
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
        set
        {
            this.RaiseAndSetIfChanged(ref selectedCylinder, value);
            SelectedCylinderChanged?.Invoke();
        }
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

[JsonDerivedType(typeof(CylinderSelector))]
public interface ICylinderSelector
{
    [JsonIgnore]
    Action SelectedCylinderChanged { get; set; }
    
    ObservableCollection<ICylinder> Cylinders { get; }
    ICylinder SetupCylinder { get; set; }
    ICylinder SelectedCylinder { get; set; }
}