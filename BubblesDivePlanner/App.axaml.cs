using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Information;
using BubblesDivePlanner.ViewModels.Plan;
using BubblesDivePlanner.ViewModels.Header;
using BubblesDivePlanner.Views;

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
                    new Information()
                ),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}