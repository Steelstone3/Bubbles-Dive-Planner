using Moq;
using Xunit;

public class HeaderShould
{
    [Fact]
    public void Construct()
    {
        // Given
        Main main = new();
        Header header = new(main);

        // Then
        Assert.NotNull(header.Help);
        Assert.NotNull(header.File);
    }
}