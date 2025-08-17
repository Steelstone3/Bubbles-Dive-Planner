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
        Mock<IDiveModelPrototype> diveModelPrototype = new();
        diveModelPrototype.Setup(dsp => dsp.DeepClone(diveStage.DiveModel)).Returns(diveModelFactory.CreateZhl16Buhlmann());
        Mock<IDiveStepPrototype> diveStepPrototype = new();
        diveStepPrototype.Setup(dsp => dsp.DeepClone(diveStage.DiveStep)).Returns(new DiveStep());
        Mock<ICylinderPrototype> cylinderPrototype = new();
        cylinderPrototype.Setup(cp => cp.DeepClone(diveStage.Cylinder)).Returns(new Cylinder());
        IDiveStagePrototype diveStagePrototype = new DiveStagePrototype(diveModelPrototype.Object, diveStepPrototype.Object, cylinderPrototype.Object);

        // When
        DiveStage clonedDiveStage = diveStagePrototype.DeepClone(diveStage);

        // Then
        Assert.NotSame(diveStage, clonedDiveStage);
        Assert.NotSame(diveStage.DiveModel, clonedDiveStage.DiveModel);
        Assert.NotSame(diveStage.DiveStep, clonedDiveStage.DiveStep);
        Assert.NotSame(diveStage.Cylinder, clonedDiveStage.Cylinder);
        diveModelPrototype.VerifyAll();
        diveStepPrototype.VerifyAll();
        cylinderPrototype.VerifyAll();
    }
}