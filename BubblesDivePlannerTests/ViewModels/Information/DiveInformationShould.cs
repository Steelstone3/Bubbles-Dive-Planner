using Moq;
using ReactiveUI;
using Xunit;

public class DiveInformationShould
{
    [Fact]
    public void Construct()
    {
        // Given
        DiveInformation diveInformation = new();

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveInformation);
        Assert.NotNull(diveInformation.DecompressionProfile);
        Assert.NotNull(diveInformation.CentralNervousSystemToxicity);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        DecompressionProfile decompressionProfile = new();
        DiveInformation diveInformation = new();
        List<string> events = new();
        diveInformation.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveInformation.DecompressionProfile = decompressionProfile;

        // Then
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveInformation.DecompressionProfile), events);
    }
}