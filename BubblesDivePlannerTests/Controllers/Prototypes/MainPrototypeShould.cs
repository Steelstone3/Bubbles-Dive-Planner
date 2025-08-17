using Xunit;

public class MainPrototypeShould
{
    [Fact]
    public void NewInstance()
    {
        // Given
        Main main = new();
        Header header = main.Header;
        DivePlan divePlan = main.DivePlan;
        DiveInformation diveInformation = main.DiveInformation;
        Result result = main.Result;
        MainPrototype mainPrototype = new();

        // When
        mainPrototype.NewInstance(main);

        // Then
        Assert.True(divePlan.DiveModelSelector.IsVisible);
        Assert.Same(header, main.Header);
        Assert.Same(divePlan, main.DivePlan);
        Assert.Same(divePlan.CylinderSelector, divePlan.CylinderSelector);
        Assert.Same(divePlan.CylinderSelector.Cylinders, divePlan.CylinderSelector.Cylinders);
        Assert.Same(diveInformation, main.DiveInformation);
        Assert.Same(result, main.Result);
    }
}