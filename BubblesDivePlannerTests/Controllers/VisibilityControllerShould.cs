using Moq;
using Xunit;

public class VisibilityControllerShould
{
    private readonly IVisibilityController visibilityController = new VisibilityController();

    [Fact]
    public void SetVisibility()
    {
        // Given
        IMain main = new Main();

        // When
        visibilityController.SetVisibility(main);

        // Then
        Assert.False(main.DiveModelSelector.IsVisible);
        Assert.False(main.CylinderSelector.SetupCylinder.IsVisible);
    }
}