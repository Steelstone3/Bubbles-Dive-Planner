using Moq;
using ReactiveUI;
using Xunit;

public class DecompressionProfileShould
{
    [Fact]
    public void Construct()
    {
        // Given
        DecompressionProfile diveInformation = new();

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(diveInformation);
        Assert.IsAssignableFrom<IDecompressionProfile>(diveInformation);
        Assert.Equal(0.0F, diveInformation.DiveCeiling);
        Assert.NotNull(diveInformation.DecompressionSteps);
        Assert.Empty(diveInformation.DecompressionSteps);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        DecompressionProfile decompressionProfile = new();
        List<string> events = new();
        decompressionProfile.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        decompressionProfile.DiveCeiling = 2.0F;

        // Then
        Assert.NotEmpty(events);
        Assert.Contains(nameof(decompressionProfile.DiveCeiling), events);
    }

    [Fact]
    public void CollectionChangedEvents()
    {
        // Given
        Mock<IDiveStep> diveStage = new();
        DecompressionProfile decompressionProfile = new();
        List<string> events = new();
        decompressionProfile.DecompressionSteps.CollectionChanged += (sender, e) => events.Add(e.NewItems.ToString());

        // When
        decompressionProfile.DecompressionSteps.Add(diveStage.Object);

        // Then
        Assert.NotNull(decompressionProfile.DecompressionSteps);
        Assert.NotEmpty(decompressionProfile.DecompressionSteps);
        Assert.NotEmpty(events);
    }
}