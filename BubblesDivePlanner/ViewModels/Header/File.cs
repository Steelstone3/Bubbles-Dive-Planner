using System.Reactive;
using ReactiveUI;

public class File
{
    private readonly IMain main;

    public File(IMain main)
    {
        this.main = main;
        NewCommand = ReactiveCommand.Create(NewInstance);
        SaveCommand = ReactiveCommand.Create(Save);
    }

    public ReactiveCommand<Unit, Unit> NewCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }

    private void NewInstance()
    {
        MainPrototype mainPrototype = new();
        mainPrototype.NewInstance(main);
    }

    private void Save()
    {
        CylinderSelectorSerialiser cylinderSelectorSerialiser = new();
        ResultSerialiser resultSerialiser = new();
        FileController fileController = new(cylinderSelectorSerialiser, resultSerialiser);

        fileController.Write(main.DivePlan.CylinderSelector, main.Result);
    }
}