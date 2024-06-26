using Xunit;

public class HeaderShould
{
    [Fact]
    public void Construct()
    {
        // Given
        Header header = new();

        // Then
        Assert.IsAssignableFrom<IHeader>(header);
        Assert.NotNull(header.Help);
    }
}