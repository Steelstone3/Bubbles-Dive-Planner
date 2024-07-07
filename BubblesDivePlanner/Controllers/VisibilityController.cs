public class VisibilityController : IVisibilityController
{
    public void SetVisibility(IMain main)
    {
        SetInvisible(main.DivePlan.DiveModelSelector);
        SetInvisible(main.DivePlan.CylinderSelector.SetupCylinder);
    }

    private void SetInvisible(IVisibility visibility)
    {
        if (visibility.IsVisible)
        {
            visibility.IsVisible = !visibility.IsVisible;
        }
    }
}

public interface IVisibilityController
{
    void SetVisibility(IMain main);
}