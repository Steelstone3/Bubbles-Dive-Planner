using Moq;
using ReactiveUI;
using Xunit;

public class GasUsageShould
{
    [Fact]
    public void Construct()
    {
        // Given
        Mock<IGasUsageValidator> gasUsageValidator = new();
        GasUsage gasUsage = new(gasUsageValidator.Object);

        // Then
        Assert.IsAssignableFrom<IGasUsage>(gasUsage);
        Assert.IsAssignableFrom<IValidation>(gasUsage);
    }

    [Fact]
    public void RaisePropertyChangedEvents()
    {
        // Given
        Mock<IGasUsageValidator> gasUsageValidator = new();
        GasUsage gasUsage = new(gasUsageValidator.Object);
        List<string> events = new();
        gasUsage.PropertyChanged += (sender, e) => events.Add(e.PropertyName);

        // When
        gasUsage.Remaining = 1680;
        gasUsage.Used = 720;
        gasUsage.SurfaceAirConsumptionRate = 12;

        // Then
        Assert.IsAssignableFrom<ReactiveObject>(gasUsage);
        Assert.NotEmpty(events);
        Assert.Contains(nameof(gasUsage.Remaining), events);
        Assert.Contains(nameof(gasUsage.Used), events);
        Assert.Contains(nameof(gasUsage.SurfaceAirConsumptionRate), events);
    }

    [Fact]
    public void Validate()
    {
        // Given
        Mock<IGasUsageValidator> gasUsageValidator = new();
        GasUsage gasUsage = new(gasUsageValidator.Object);
        gasUsageValidator.Setup(guv => guv.Validate(gasUsage)).Returns(true);

        // When
        bool isValid = gasUsage.IsValid;

        // Then
        Assert.True(isValid);
        gasUsageValidator.VerifyAll();
    }
}