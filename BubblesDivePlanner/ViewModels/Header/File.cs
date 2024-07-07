using System.Reactive;
using ReactiveUI;

public class File
{
    private readonly IMain main;

    public File(IMain main)
    {
        this.main = main;
        NewCommand = ReactiveCommand.Create(NewInstance);
    }

    public ReactiveCommand<Unit, Unit> NewCommand { get; }

    private void NewInstance()
    {
        MainPrototype mainPrototype = new();
        mainPrototype.NewInstance(main);
    }
}