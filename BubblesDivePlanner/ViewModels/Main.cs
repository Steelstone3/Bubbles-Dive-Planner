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

    public void New(Main main)
    {
        // Nuke these
        main.DivePlan.DiveStage = new DiveStage();
        main.DiveInformation = new DiveInformation();
        main.Result.Results.Clear();

        // Neat these
        main.DivePlan.DiveModelSelector.IsVisible = true;
        main.DivePlan.CylinderSelector.New(main.DivePlan.CylinderSelector);
    }

    // TODO AH temporary whilst CanCalculateDiveStage is not implemented
    private void CalculateDiveStage()
    {
        if (DivePlan.DiveModelSelector.DiveModelSelected == null || DivePlan.CylinderSelector.SelectedCylinder == null)
        {
            return;
        }

        DivePlan.DiveStage.DiveModel = DivePlan.DiveModelSelector.DiveModelSelected;
        DivePlan.DiveStage.Cylinder = DivePlan.CylinderSelector.SelectedCylinder;

        DiveStageValidator diveStageValidator = new();

        if (DivePlan.DiveStage.Cylinder == null || !diveStageValidator.IsValid(DivePlan.DiveStage))
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

        DiveProfileStagesCommand diveProfileStagesCommand = new();

        diveProfileStagesCommand.Run(DivePlan.DiveStage);
        Result.Results.Add(new DiveStage(DivePlan.DiveStage));
    }

    private void CalculateDiveBoundaries()
    {
        if (DivePlan.DiveStage.DiveModel == null)
        {
            return;
        }

        DivePlan.DiveStage.Cylinder = DivePlan.CylinderSelector.SelectedCylinder;

        DiveBoundaryController diveBoundaryController = new();
        DecompressionController decompressionController = new();

        DiveInformation.DecompressionProfile.DiveCeiling = diveBoundaryController.GetOverallDiveCeiling(Result.Results);
        DiveInformation.DecompressionProfile.DecompressionSteps.Clear();
        List<DiveStep> decompressionSteps = decompressionController.CollateDecompressionDiveSteps(new DiveStage(DivePlan.DiveStage));

        foreach (var decompressionStep in decompressionSteps)
        {
            DiveInformation.DecompressionProfile.DecompressionSteps.Add(decompressionStep);
        }
    }
}
