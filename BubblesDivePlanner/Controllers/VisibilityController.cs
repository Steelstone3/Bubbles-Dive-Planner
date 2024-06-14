public class VisibilityController : IVisibilityController
{
    public void SetVisibility(IMain main)
    {
        SetInvisible(main.DiveModelSelector);
        SetInvisible(main.CylinderSelector.SetupCylinder);
    }

    // public void SetDefaultVisibility(IMain main)
    // {
    //     SetVisible(main.DiveModelSelector);
    //     SetVisible(main.CylinderSelector.SetupCylinder);
    // }

    private void SetVisible(IVisibility visibility)
    {
        if (!visibility.IsVisible)
        {
            visibility.IsVisible = !visibility.IsVisible;
        }
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
    // void SetDefaultVisibility(IMain main);
}