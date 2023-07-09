using BubblesDivePlanner.ViewModels.Models;
using BubblesDivePlanner.ViewModels.Models.DiveInformation;
using BubblesDivePlanner.ViewModels.Models.DivePlan;
using BubblesDivePlanner.ViewModels.Models.Header;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public MainWindowViewModel(IHeaderModel header, IDivePlanModel divePlan, IDiveInformationModel diveInformation)
        {
            Header = header;
            Plan = divePlan;
            Information = diveInformation;
        }

        private IHeaderModel header;
        public IHeaderModel Header
        {
            get => header;
            set => this.RaiseAndSetIfChanged(ref header, value);
        }

        private IDivePlanModel plan;
        public IDivePlanModel Plan
        {
            get => plan;
            set => this.RaiseAndSetIfChanged(ref plan, value);
        }

        private IDiveInformationModel information;
        public IDiveInformationModel Information
        {
            get => information;
            set => this.RaiseAndSetIfChanged(ref information, value);
        }
    }
}