using System.Reactive;
using ReactiveUI;

public class Main : ReactiveObject, IMain
{
    public Main()
    {
        CalculateCommand = ReactiveCommand.Create(CalculateDiveStage); //, CanCalculateDiveStage);
    }

    private IDivePlan divePlan = new DivePlan();
    public IDivePlan DivePlan
    {
        get => divePlan;
        set => this.RaiseAndSetIfChanged(ref divePlan, value);
    }

    private IResult result = new Result();
    public IResult Result
    {
        get => result;
        set => this.RaiseAndSetIfChanged(ref result, value);
    }

    public ReactiveCommand<Unit, Unit> CalculateCommand { get; }

    // public IObservable<bool> CanCalculateDiveStage { get; }

    private void CalculateDiveStage()
    {
        DivePlan.DiveStage.DiveModel = DivePlan.DiveModelSelector.DiveModelSelected;
        DivePlan.DiveStage.Cylinder = DivePlan.CylinderSelector.SelectedCylinder;

        // TODO AH temporary whilst CanCalculateDiveStage is not implemented
        if (!DivePlan.DiveStage.IsValid)
        {
            return;
        }

        VisibilityController visibilityController = new();
        CylinderController cylinderController = new();
        DiveProfileStagesFactory diveProfileStagesFactory = new();
        DiveModelPrototype diveModelPrototype = new();
        DiveStepPrototype diveStepPrototype = new();
        CylinderPrototype cylinderPrototype = new();
        DiveStagePrototype diveStagePrototype = new(diveModelPrototype, diveStepPrototype, cylinderPrototype);

        visibilityController.SetVisibility(this);

        DivePlan.DiveStage.Cylinder.GasUsage = cylinderController.UpdateGasUsage(DivePlan.DiveStage.DiveStep, DivePlan.DiveStage.Cylinder.GasUsage);

        diveProfileStagesFactory.Run(DivePlan.DiveStage);
        Result.Results.Add(diveStagePrototype.DeepClone(DivePlan.DiveStage));
    }
}

public interface IMain
{
    public IDivePlan DivePlan
    {
        get;
        set;
    }

    public IResult Result
    {
        get;
        set;
    }
}
