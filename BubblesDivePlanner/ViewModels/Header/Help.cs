using System.Reactive;
using ReactiveUI;

public class Help : IHelp
{
    public Help()
    {
        HelpCommand = ReactiveCommand.Create(OpenHelp);
        AboutCommand = ReactiveCommand.Create(OpenAbout);
    }

    public ReactiveCommand<Unit, Unit> HelpCommand { get; }
    public ReactiveCommand<Unit, Unit> AboutCommand { get; }

    private void OpenHelp()
    {
    }

    private void OpenAbout()
    {
        throw new NotImplementedException();
    }
}

public interface IHelp
{

}