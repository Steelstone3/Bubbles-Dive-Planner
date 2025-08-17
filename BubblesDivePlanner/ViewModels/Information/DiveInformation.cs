using ReactiveUI;

public class DiveInformation : ReactiveObject
{
    private DecompressionProfile decompressionProfile = new DecompressionProfile();
    public DecompressionProfile DecompressionProfile
    {
        get => decompressionProfile;
        set => this.RaiseAndSetIfChanged(ref decompressionProfile, value);
    }

    public CentralNervousSystemToxicity CentralNervousSystemToxicity { get; } = new CentralNervousSystemToxicity();
}