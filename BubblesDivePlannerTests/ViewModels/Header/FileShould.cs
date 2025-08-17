using Xunit;

public class FileShould
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
        File file = new(main);

        // When
        file.NewCommand.Execute().Subscribe();

        // Then
        Assert.True(divePlan.DiveModelSelector.IsVisible);
        Assert.Same(header, main.Header);
        Assert.Same(divePlan, main.DivePlan);
        Assert.Same(diveInformation, main.DiveInformation);
        Assert.Same(result, main.Result);
    }
}