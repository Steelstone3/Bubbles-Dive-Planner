using Moq;
using Xunit;

public class DiveStagePrototypeShould
{
    [Fact]
    public void DeepClone()
    {
        // Given
        DiveModelFactory diveModelFactory = new();
        DiveStage diveStage = new();
        diveStage.DiveModel = diveModelFactory.CreateZhl16Buhlmann();
        IDiveStagePrototype diveStagePrototype = new DiveStagePrototype();

        // When
        DiveStage clonedDiveStage = diveStagePrototype.DeepClone(diveStage);

        // Then
        Assert.NotSame(diveStage, clonedDiveStage);
        Assert.NotSame(diveStage.DiveModel, clonedDiveStage.DiveModel);
        Assert.NotSame(diveStage.DiveStep, clonedDiveStage.DiveStep);
        Assert.NotSame(diveStage.Cylinder, clonedDiveStage.Cylinder);
    }
}