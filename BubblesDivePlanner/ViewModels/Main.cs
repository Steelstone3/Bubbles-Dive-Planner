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
        DiveStage.DiveModel = DiveModelSelector.DiveModelSelected;
        new DiveProfileStagesFactory().Run(DiveStage);
        Results.LatestResult = new DiveStageClone().Clone(DiveStage);
    }
}

public interface IMain
{
    IDiveModelSelector DiveModelSelector { get; set; }
    IDiveStage DiveStage { get; set; }
}
