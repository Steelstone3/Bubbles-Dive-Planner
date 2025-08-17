using Xunit;

public class CylinderPrototypeShould
{
    private readonly ICylinderPrototype cylinderPrototype = new CylinderPrototype();

    [Fact]
    public void DeepClone()
    {
        // Given
        Cylinder cylinder = new Cylinder();

        // When
        Cylinder clonedCylinder = cylinderPrototype.DeepClone(cylinder);

        // Then
        Assert.NotSame(cylinder, clonedCylinder);
        Assert.NotSame(cylinder.GasMixture, clonedCylinder.GasMixture);
        Assert.NotSame(cylinder.GasUsage, clonedCylinder.GasUsage);
    }
}