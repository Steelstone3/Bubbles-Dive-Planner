using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BubblesDivePlanner.ViewModels.Informations;
using BubblesDivePlanner.ViewModels.Headers;
using BubblesDivePlanner.ViewModels.Plan;

namespace BubblesDivePlanner;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Views.MainWindow
            {
                DataContext = new ViewModels.MainWindow
                (
                    new Header(),
                    new Planner(),
                    new DiveInformation()
                ),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}