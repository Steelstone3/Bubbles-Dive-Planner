using System.Reactive;
using ReactiveUI;

public class Main : ReactiveObject
{
    public Main()
    {
        CalculateCommand = ReactiveCommand.Create(CalculateDiveStage); //, CanCalculateDiveStage);
        DivePlan.CylinderSelector.SelectedCylinderChanged = () => CalculateDiveBoundaries();
        Header = new Header(this);
    }

    public Header Header
    {
        get;
    }

    private DivePlan divePlan = new();
    public DivePlan DivePlan
    {
        get => divePlan;
        set => this.RaiseAndSetIfChanged(ref divePlan, value);
    }

    private DiveInformation diveInformation = new DiveInformation();
    public DiveInformation DiveInformation
    {
        get => diveInformation;
        set => this.RaiseAndSetIfChanged(ref diveInformation, value);
    }

    private Result result = new Result();
    public Result Result
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
        DiveStageValidator diveStageValidator = new();
        if (DivePlan.DiveStage.Cylinder == null || !diveStageValidator.Validate(DivePlan.DiveStage))
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

        DiveProfileStagesCommand diveProfileStagesCommand = new();

        diveProfileStagesCommand.Run(DivePlan.DiveStage);
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
        List<DiveStep> decompressionSteps = decompressionController.CollateDecompressionDiveSteps(diveStagePrototype.DeepClone(DivePlan.DiveStage));

        foreach (var decompressionStep in decompressionSteps)
        {
            DiveInformation.DecompressionProfile.DecompressionSteps.Add(decompressionStep);
        }
    }
}
