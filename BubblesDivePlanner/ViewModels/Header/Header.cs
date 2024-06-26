public class Header : IHeader
{
    public IHelp Help
    {
        get;
    } = new Help();
}

public interface IHeader
{
    IHelp Help
    {
        get;
    }
}