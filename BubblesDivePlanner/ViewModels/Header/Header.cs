public class Header
{
    public Header(Main main)
    {
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
