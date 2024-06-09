using Moq;
using Xunit;

public class VisibilityControllerShould
{
    private readonly IVisibilityController visibilityController = new VisibilityController();

 [Fact]
    public void SetVisible()
    {
        // TODO AH Make the cylinder setup and dive model visible by default
        // TODO AH Invisibile once the calculate is ran
        // TODO AH Make both read only views invisibile until first calculation
        // Given
        Mock<ICylinderValidator> cylinderValidator = new();
        Mock<ICylinderController> cylinderController = new();
        Cylinder cylinder = new(cylinderValidator.Object, cylinderController.Object);
        cylinder.IsVisible = false;

        // When
        visibilityController.SetVisible(cylinder);

        // Then
        Assert.True(cylinder.IsVisible);
    }

    [Fact]
    public void SetInvisible()
    {
        // TODO AH Make the cylinder setup and dive model visible by default
        // TODO AH Invisibile once the calculate is ran
        // TODO AH Make both read only views invisibile until first calculation
        // Given
        Mock<ICylinderValidator> cylinderValidator = new();
        Mock<ICylinderController> cylinderController = new();
        Cylinder cylinder = new(cylinderValidator.Object, cylinderController.Object);
        cylinder.IsVisible = false;

        // When
        visibilityController.SetInvisible(cylinder);

        // Then
        Assert.False(cylinder.IsVisible);
    }
}