using Xunit;

public class DiveModelPrototypeShould
{
    [Fact]
    public void DeepClone()
    {
        // Given
        DiveModelFactory diveModelFactory = new();
        IDiveModelPrototype diveModelPrototype = new DiveModelPrototype();
        DiveModel diveModel = diveModelFactory.CreateZhl16Buhlmann();

        // When
        DiveModel clonedDiveModel = diveModelPrototype.DeepClone(diveModel);

        // Then
        Assert.NotSame(diveModel, clonedDiveModel);
        Assert.NotSame(diveModel.DiveModelProfile, clonedDiveModel.DiveModelProfile);
    }
}