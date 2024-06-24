using Xunit;

public class DiveModelPrototypeShould
{
    [Fact]
    public void DeepClone()
    {
        // Given
        IDiveModelPrototype diveModelPrototype = new DiveModelPrototype();
        IDiveModel diveModel = new Zhl16Buhlmann();

        // When
        IDiveModel clonedDiveModel = diveModelPrototype.DeepClone(diveModel);

        // Then
        Assert.NotSame(diveModel, clonedDiveModel);
    }
}