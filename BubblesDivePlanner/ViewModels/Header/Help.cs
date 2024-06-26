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
        HelpView helpView = new();
        helpView.Show();
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