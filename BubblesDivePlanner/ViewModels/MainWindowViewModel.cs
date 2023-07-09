using BubblesDivePlanner.ViewModels.Models;
using BubblesDivePlanner.ViewModels.Models.DiveInformation;
using BubblesDivePlanner.ViewModels.Models.DivePlan;
using BubblesDivePlanner.ViewModels.Models.Header;

namespace BubblesDivePlanner.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public MainWindowViewModel(IHeaderModel header, IDivePlanModel divePlan, IDiveInformationModel diveInformation)
        {
            Header = header;
            DivePlan = divePlan;
            DiveInformation = diveInformation;
        }

        public static string Greeting => "Welcome to Avalonia!";
        public IHeaderModel Header { get; set; }
        public IDivePlanModel DivePlan { get; set; }
        public IDiveInformationModel DiveInformation { get; set; }
    }
}