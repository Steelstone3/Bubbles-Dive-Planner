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

        DiveProfileStagesFactory diveProfileStagesFactory = new();
        diveProfileStagesFactory.Run(DiveStage);
        Results.LatestResult = DiveStage;
    }
}

public interface IMain
{
    IDiveModelSelector DiveModelSelector { get; set; }
    IDiveStage DiveStage { get; set; }
}
