using Moq;
using Xunit;

public class DiveStagePrototypeShould
{

    [Fact]
    public void DeepClone()
    {
        // Given
        Mock<IDiveStageValidator> diveStageValidator = new();
        IDiveStage diveStage = new DiveStage(diveStageValidator.Object);
        Mock<IDiveStepPrototype> diveStepPrototype = new();
        diveStepPrototype.Setup(dsp => dsp.DeepClone(diveStage.DiveStep)).Returns(new DiveStep(new DiveStepValidator()));
        Mock<ICylinderPrototype> cylinderPrototype = new();
        cylinderPrototype.Setup(cp => cp.DeepClone(diveStage.Cylinder)).Returns(new Cylinder(new CylinderValidator(), new CylinderController()));
        IDiveStagePrototype diveStagePrototype = new DiveStagePrototype(diveStepPrototype.Object, cylinderPrototype.Object);

        // When
        IDiveStage clonedDiveStage = diveStagePrototype.DeepClone(diveStage);

        // Then
        Assert.NotSame(diveStage, clonedDiveStage);
        diveStepPrototype.VerifyAll();
        cylinderPrototype.VerifyAll();
    }
}