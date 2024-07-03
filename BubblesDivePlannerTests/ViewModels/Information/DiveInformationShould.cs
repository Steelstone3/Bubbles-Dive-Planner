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
        Assert.IsAssignableFrom<IDiveInformation>(diveInformation);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IDecompressionProfile> decompressionProfile = new();
        DiveInformation diveInformation = new();
        List<string> events = new();
        diveInformation.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        diveInformation.DecompressionProfile = decompressionProfile.Object;

        // Then
        Assert.NotEmpty(events);
        Assert.Contains(nameof(diveInformation.DecompressionProfile), events);
    }
}