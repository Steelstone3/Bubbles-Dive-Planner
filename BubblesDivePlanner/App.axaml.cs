using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.DiveInformation;
using BubblesDivePlanner.ViewModels.DivePlan;
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
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel
                (
                    new HeaderViewModel(),
                    new DivePlanViewModel(),
                    new DiveInformationViewModel()
                ),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}