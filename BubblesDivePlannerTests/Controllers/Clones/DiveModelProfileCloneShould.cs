using Moq;
using Xunit;

public class DiveModelProfileCloneShould
{
    [Fact]
    public void Clone()
    {
        // Given
        Mock<IDiveModelProfile> diveModelProfile = new();
        DiveModelProfileClone diveModelProfileClone = new();

        // When
        IDiveModelProfile newDiveModelProfile = diveModelProfileClone.Clone(diveModelProfile.Object);

        // Then
        Assert.NotNull(newDiveModelProfile);
        Assert.NotSame(diveModelProfile.Object, newDiveModelProfile);
    }
}