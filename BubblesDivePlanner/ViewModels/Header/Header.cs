public class Header : IHeader
{
    private IMain main;

    public Header(IMain main)
    {
        this.main = main;
        File = new File(main);
    }

    public File File
    {
        get;
    }

    public Help Help
    {
        get;
    } = new Help();
}

public interface IHeader
{
    File File
    {
        get;
    }

    Help Help
    {
        get;
    }
}