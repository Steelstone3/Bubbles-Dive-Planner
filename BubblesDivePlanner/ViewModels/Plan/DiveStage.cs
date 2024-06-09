using ReactiveUI;

public class DiveStage : ReactiveObject, IDiveStage
{
    private readonly IDiveStageValidator diveStageValidator;

    public DiveStage(IDiveStageValidator diveStageValidator)
    {
        this.diveStageValidator = diveStageValidator;
    }

    private IDiveModel diveModel;
    public IDiveModel DiveModel
    {
        get => diveModel;
        set => this.RaiseAndSetIfChanged(ref diveModel, value);
    }

    private IDiveStep diveStep = new DiveStep(new DiveStepValidator());
    public IDiveStep DiveStep
    {
        get => diveStep;
        set => this.RaiseAndSetIfChanged(ref diveStep, value);
    }

    private ICylinder cylinder = new Cylinder(new CylinderValidator(), new CylinderController());
    public ICylinder Cylinder
    {
        get => cylinder;
        set => this.RaiseAndSetIfChanged(ref cylinder, value);
    }

    public bool IsValid => diveStageValidator.Validate(this);
}

public interface IDiveStage : IValidation
{
    IDiveModel DiveModel { get; set; }
    IDiveStep DiveStep { get; set; }
    ICylinder Cylinder { get; set; }
}