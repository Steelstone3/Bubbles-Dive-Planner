using Moq;
using Xunit;

public class DiveStageValidatorShould
{
    [Fact]
    public void ValidateDiveStage()
    {
        // Given
        Mock<IDiveStep> diveStep = new();
        diveStep.Setup(ds => ds.IsValid).Returns(true);
        Mock<ICylinder> cylinder = new();
        cylinder.Setup(c => c.IsValid).Returns(true);
        Mock<IDiveStage> diveStage = new();
        diveStage.Setup(ds => ds.DiveStep).Returns(diveStep.Object);
        diveStage.Setup(ds => ds.Cylinder).Returns(cylinder.Object);
        DiveStageValidator diveStageValidator = new();

        // When
        bool isValid = diveStageValidator.Validate(diveStage.Object);

        // Then
        Assert.True(isValid);
        diveStage.VerifyAll();
    }
}