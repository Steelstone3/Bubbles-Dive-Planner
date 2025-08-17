using Moq;
using Xunit;

public class VisibilityControllerShould
{
    private readonly IVisibilityController visibilityController = new VisibilityController();

    [Fact]
    public void SetVisibility()
    {
        // Given
        Main main = new Main();

        // When
        visibilityController.SetVisibility(main);

        // Then
        Assert.False(main.DivePlan.DiveModelSelector.IsVisible);
        Assert.False(main.DivePlan.CylinderSelector.SetupCylinder.IsVisible);
    }
}