using System.Reactive;
using ReactiveUI;

public class Main : ReactiveObject, IMain
{
    private readonly DiveProfileStagesFactory diveProfileStagesFactory;

    public Main()
    {
        diveProfileStagesFactory = new DiveProfileStagesFactory();
        CalculateCommand = ReactiveCommand.Create(CalculateDiveStage); //, CanCalculateDiveStage);
    }

    private IDiveModelSelector diveModelSelector = new DiveModelSelector();
    public IDiveModelSelector DiveModelSelector
    {
        get => diveModelSelector;
        set => this.RaiseAndSetIfChanged(ref diveModelSelector, value);
    }

    private IDiveStage diveStage = new DiveStage();
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
        // TODO AH temporary whilst CanCalculateDiveStage is not implemented
        if (!DiveStage.IsValid)
        {
            return;
        }

        DiveStage.DiveModel = DiveModelSelector.DiveModelSelected;
        diveProfileStagesFactory.Run(DiveStage);
        Results.LatestResult = DiveStage;
    }
}

public interface IMain
{
    IDiveModelSelector DiveModelSelector { get; set; }
    IDiveStage DiveStage { get; set; }
}
