using System.Reactive;
using ReactiveUI;

public class Main : ReactiveObject, IMain
{
    public Main()
    {
        CalculateCommand = ReactiveCommand.Create(CalculateDiveStage); //, CanCalculateDiveStage);
    }

    private IDiveModelSelector diveModelSelector = new DiveModelSelector();
    public IDiveModelSelector DiveModelSelector
    {
        get => diveModelSelector;
        set => this.RaiseAndSetIfChanged(ref diveModelSelector, value);
    }

    private ICylinderSelector cylinderSelector = new CylinderSelector();
    public ICylinderSelector CylinderSelector
    {
        get => cylinderSelector;
        set => this.RaiseAndSetIfChanged(ref cylinderSelector, value);
    }

    private IDiveStage diveStage = new DiveStage(new DiveStageValidator());
    public IDiveStage DiveStage
    {
        get => diveStage;
        set => this.RaiseAndSetIfChanged(ref diveStage, value);
    }

    private IResults results = new Results();
    public IResults Results
    {
        get => results;
        set => this.RaiseAndSetIfChanged(ref results, value);
    }

    public ReactiveCommand<Unit, Unit> CalculateCommand { get; }

    // public IObservable<bool> CanCalculateDiveStage { get; }

    private void CalculateDiveStage()
    {
        DiveStage.DiveModel = DiveModelSelector.DiveModelSelected;
        DiveStage.Cylinder = CylinderSelector.SelectedCylinder;

        // TODO AH temporary whilst CanCalculateDiveStage is not implemented
        if (!DiveStage.IsValid)
        {
            return;
        }

        VisibilityController visibilityController = new();
        visibilityController.SetVisibility(this);

        CylinderController cylinderController = new();
        DiveStage.Cylinder.GasUsage = cylinderController.UpdateGasUsage(DiveStage.DiveStep, DiveStage.Cylinder.GasUsage);

        DiveProfileStagesFactory diveProfileStagesFactory = new();
        diveProfileStagesFactory.Run(DiveStage);
        Results.LatestResult = diveStage;
        // Results.LatestResult = new DiveStagePrototype(new DiveStepPrototype(), new CylinderPrototype()).DeepClone(DiveStage);
    }
}

public interface IMain
{
    // TODO AH IPlan
    public IDiveModelSelector DiveModelSelector
    {
        get;
        set;
    }

    public ICylinderSelector CylinderSelector
    {
        get;
        set;
    }

    public IDiveStage DiveStage
    {
        get;
        set;
    }
    // TODO AH Up to here

    public IResults Results
    {
        get;
        set;
    }
}
