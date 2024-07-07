using Moq;
using Xunit;

public class HeaderShould
{
    [Fact]
    public void Construct()
    {
        // Given
        Mock<IMain> main = new();
        Header header = new(main.Object);

        // Then
        Assert.IsAssignableFrom<IHeader>(header);
        Assert.NotNull(header.Help);
        Assert.NotNull(header.File);
    }
}