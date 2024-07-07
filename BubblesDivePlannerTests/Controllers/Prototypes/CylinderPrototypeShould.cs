using Moq;
using Xunit;

public class CylinderPrototypeShould
{
    private readonly ICylinderPrototype cylinderPrototype = new CylinderPrototype();

    [Fact]
    public void DeepClone()
    {
        // Given
        Mock<ICylinderValidator> cylinderValidator = new();
        Mock<ICylinderController> cylinderController = new();
        ICylinder cylinder = new Cylinder(cylinderValidator.Object, cylinderController.Object);

        // When
        ICylinder clonedCylinder = cylinderPrototype.DeepClone(cylinder);

        // Then
        Assert.NotSame(cylinder, clonedCylinder);
        Assert.NotSame(cylinder.GasMixture, clonedCylinder.GasMixture);
        Assert.NotSame(cylinder.GasUsage, clonedCylinder.GasUsage);
    }
}