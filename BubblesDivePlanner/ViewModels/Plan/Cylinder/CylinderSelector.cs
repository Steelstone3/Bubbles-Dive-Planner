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

    public void New(CylinderSelector cylinderSelector)
    {
        cylinderSelector.SelectedCylinder = null;
        cylinderSelector.Cylinders.Clear();
        cylinderSelector.SetupCylinder = new Cylinder();
    }

    private void AddCylinder()
    {
        if (SetupCylinder == null)
        {
            return;
        }

        CylinderValidator cylinderValidator = new();
        CylinderController cylinderController = new();
        DiveBoundaryController diveBoundaryController = new();
        SetupCylinder.InitialPressurisedVolume = cylinderController.CalculateInitialPressurisedVolume(SetupCylinder.Volume, SetupCylinder.Pressure);
        SetupCylinder.GasMixture.Nitrogen = cylinderController.CalculateNitrogen(SetupCylinder.GasMixture.Oxygen, SetupCylinder.GasMixture.Helium);
        SetupCylinder.GasMixture.MaximumOperatingDepth = diveBoundaryController.CalculateMaximumOperatingDepth(SetupCylinder.GasMixture.Oxygen);

        if (!cylinderValidator.IsValid(SetupCylinder))
        {
            return;
        }

        Cylinder clonedSelectedCylinder = new Cylinder(SetupCylinder);
        Cylinders.Add(clonedSelectedCylinder);
    }
}