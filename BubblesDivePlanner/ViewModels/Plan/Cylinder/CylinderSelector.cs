using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;

public class CylinderSelector : ReactiveObject
{
    public CylinderSelector()
    {
        AddCylinderCommand = ReactiveCommand.Create(AddCylinder); //, CanAddCylinder);
    }

    public Action SelectedCylinderChanged { get; set; }

    public ReactiveCommand<Unit, Unit> AddCylinderCommand { get; }

    public ObservableCollection<Cylinder> Cylinders
    {
        get;
    } = new ObservableCollection<Cylinder>();

    private Cylinder setupCylinder = new Cylinder();
    public Cylinder SetupCylinder
    {
        get => setupCylinder;
        set => this.RaiseAndSetIfChanged(ref setupCylinder, value);
    }

    private Cylinder selectedCylinder;
    public Cylinder SelectedCylinder
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
        if (SetupCylinder == null)
        {
            return;
        }
        
        CylinderValidator cylinderValidator = new();

        // TODO AH Calculate gas mixture
        // TODO AH Calculate initial pressure
        CylinderController cylinderController = new();


        SetupCylinder.InitialPressurisedVolume = cylinderController.CalculateInitialPressurisedVolume(SetupCylinder.Volume, SetupCylinder.Pressure);

        if (!cylinderValidator.Validate(SetupCylinder))
        {
            return;
        }

        ICylinderPrototype cylinderPrototype = new CylinderPrototype();
        Cylinder clonedSelectedCylinder = cylinderPrototype.DeepClone(SetupCylinder);
        Cylinders.Add(clonedSelectedCylinder);
    }
}