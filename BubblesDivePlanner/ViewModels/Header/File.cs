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
        LoadCommand = ReactiveCommand.Create(Load);
    }

    public ReactiveCommand<Unit, Unit> NewCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> LoadCommand { get; }

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

        fileController.Write(main);
    }

    private void Load()
    {
        CylinderSelectorSerialiser cylinderSelectorSerialiser = new();
        ResultSerialiser resultSerialiser = new();
        FileController fileController = new(cylinderSelectorSerialiser, resultSerialiser);

        fileController.Read(main);
    }
}