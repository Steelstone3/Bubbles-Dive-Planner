public class VisibilityController : IVisibilityController
{
    public void SetInvisible(IVisibility visibility)
    {
        if(visibility.IsVisible)
        {
            visibility.IsVisible = !visibility.IsVisible;
        }
    }

    public void SetVisible(IVisibility visibility)
    {
        if(!visibility.IsVisible)
        {
            visibility.IsVisible = !visibility.IsVisible;
        }
    }
}

public interface IVisibilityController
{
    void SetInvisible(IVisibility visibility);
    void SetVisible(IVisibility visibility);
}