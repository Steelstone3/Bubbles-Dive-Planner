using ReactiveUI;

public class DiveInformation : ReactiveObject, IDiveInformation
{
    private IDecompressionProfile decompressionProfile = new DecompressionProfile();
    public IDecompressionProfile DecompressionProfile
    {
        get => decompressionProfile;
        set => this.RaiseAndSetIfChanged(ref decompressionProfile, value);
    }

    public ICentralNervousSystemToxicity CentralNervousSystemToxicity { get; } = new CentralNervousSystemToxicity();
}

public interface IDiveInformation
{
    IDecompressionProfile DecompressionProfile { get; set; }
    ICentralNervousSystemToxicity CentralNervousSystemToxicity { get; }
}