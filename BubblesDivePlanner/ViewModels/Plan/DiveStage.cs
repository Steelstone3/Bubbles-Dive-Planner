using ReactiveUI;

public class DiveStage : ReactiveObject, IDiveStage
{
    private IDiveModel diveModel;
    public IDiveModel DiveModel
    {
        get => diveModel;
        set => this.RaiseAndSetIfChanged(ref diveModel, value);
    }

    private IDiveStep diveStep = new DiveStep();
    public IDiveStep DiveStep
    {
        get => diveStep;
        set => this.RaiseAndSetIfChanged(ref diveStep, value);
    }

    private IGasMixture gasMixture = new GasMixture();
    public IGasMixture GasMixture
    {
        get => gasMixture;
        set => this.RaiseAndSetIfChanged(ref gasMixture, value);
    }

    // TODO AH Test
    public bool IsValid => DiveStep.IsValid && GasMixture.IsValid;
}

public interface IDiveStage : IValidation
{
    IDiveModel DiveModel { get; set; }
    IDiveStep DiveStep { get; set; }
    IGasMixture GasMixture { get; set; }
}