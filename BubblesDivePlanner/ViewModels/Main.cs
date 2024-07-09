using System.Reactive;
using ReactiveUI;

public class Main : ReactiveObject, IMain
{
    public Main()
    {
        CalculateCommand = ReactiveCommand.Create(CalculateDiveStage); //, CanCalculateDiveStage);
        ((CylinderSelector)DivePlan.CylinderSelector).SelectedCylinderChanged = () => CalculateDiveBoundaries();
        Header = new Header(this);
    }

    public IHeader Header
    {
        get;
    }

    private IDivePlan divePlan = new DivePlan();
    public IDivePlan DivePlan
    {
        get => divePlan;
        set => this.RaiseAndSetIfChanged(ref divePlan, value);
    }

    private IDiveInformation diveInformation = new DiveInformation();
    public IDiveInformation DiveInformation
    {
        get => diveInformation;
        set => this.RaiseAndSetIfChanged(ref diveInformation, value);
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

        // Visibility
        ToggleVisibility();

        // Results
        CalculateDiveResults();

        // Dive Boundaries
        CalculateDiveBoundaries();
    }

    private void ToggleVisibility()
    {
        VisibilityController visibilityController = new();

        visibilityController.SetVisibility(this);
    }

    private void CalculateDiveResults()
    {
        CylinderController cylinderController = new();

        DivePlan.DiveStage.Cylinder.GasUsage = cylinderController.UpdateGasUsage(DivePlan.DiveStage.DiveStep, DivePlan.DiveStage.Cylinder.GasUsage);

        DiveModelPrototype diveModelPrototype = new();
        DiveStepPrototype diveStepPrototype = new();
        CylinderPrototype cylinderPrototype = new();
        DiveStagePrototype diveStagePrototype = new(diveModelPrototype, diveStepPrototype, cylinderPrototype);

        DiveProfileStagesFactory diveProfileStagesFactory = new();

        diveProfileStagesFactory.Run(DivePlan.DiveStage);
        Result.Results.Add(diveStagePrototype.DeepClone(DivePlan.DiveStage));
    }

    private void CalculateDiveBoundaries()
    {
        if (DivePlan.DiveStage.DiveModel == null)
        {
            return;
        }

        DivePlan.DiveStage.Cylinder = DivePlan.CylinderSelector.SelectedCylinder;

        DiveModelPrototype diveModelPrototype = new();
        DiveStepPrototype diveStepPrototype = new();
        CylinderPrototype cylinderPrototype = new();
        DiveStagePrototype diveStagePrototype = new(diveModelPrototype, diveStepPrototype, cylinderPrototype);

        DiveBoundaryController diveBoundaryController = new();
        DecompressionController decompressionController = new();

        DiveInformation.DecompressionProfile.DiveCeiling = diveBoundaryController.GetOverallDiveCeiling(Result.Results);
        DiveInformation.DecompressionProfile.DecompressionSteps.Clear();
        List<IDiveStep> decompressionSteps = decompressionController.CollateDecompressionDiveSteps(diveStagePrototype.DeepClone(DivePlan.DiveStage));

        foreach (var decompressionStep in decompressionSteps)
        {
            DiveInformation.DecompressionProfile.DecompressionSteps.Add(decompressionStep);
        }
    }
}

public interface IMain
{
    public IHeader Header
    {
        get;
    }

    public IDivePlan DivePlan
    {
        get;
        set;
    }

    public IDiveInformation DiveInformation
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
