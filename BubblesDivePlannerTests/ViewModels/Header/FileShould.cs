using Xunit;

public class FileShould
{
    [Fact]
    public void NewInstance()
    {
        // Given
        Main main = new();
        IHeader header = main.Header;
        IDivePlan divePlan = main.DivePlan;
        IDiveInformation diveInformation = main.DiveInformation;
        IResult result = main.Result;
        File file = new(main);

        // When
        file.NewCommand.Execute().Subscribe();
    
        // Then
        Assert.Same(header, main.Header);
        Assert.NotSame(divePlan, main.DivePlan);
        Assert.NotSame(diveInformation, main.DiveInformation);
        Assert.NotSame(result, main.Result);
    }
}