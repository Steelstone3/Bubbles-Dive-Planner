public class MainPrototype : IMainPrototype
{
    public void NewInstance(IMain main)
    {
        main.DivePlan = new DivePlan();
        main.DiveInformation = new DiveInformation();
        main.Result = new Result();
    }
}

public interface IMainPrototype
{
    void NewInstance(IMain main);
}