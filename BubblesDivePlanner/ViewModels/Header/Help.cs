using System.Reactive;
using BubblesDivePlanner.Views.Header;
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
        throw new NotImplementedException();
    }

    private void OpenAbout()
    {
        AboutView aboutView = new();
        aboutView.Show();
    }
}

public interface IHelp
{

}