using Xunit;

public class DiveModelProfilePrototypeShould
{
    [Fact]
    public void DeepClone()
    {
        // Given
        IDiveModelProfilePrototype diveModelProfilePrototype = new DiveModelProfilePrototype();
        IDiveModelProfile diveModelProfile = new DiveModelProfile(16);

        // When
        IDiveModelProfile clonedDiveModelProfile = diveModelProfilePrototype.DeepClone(diveModelProfile);

        // Then
        Assert.NotSame(diveModelProfile, clonedDiveModelProfile);
    }
}