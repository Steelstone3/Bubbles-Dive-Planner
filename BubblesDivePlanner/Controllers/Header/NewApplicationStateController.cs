using BubblesDivePlanner.Services;
using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.DiveApplication;
using BubblesDivePlanner.ViewModels.DiveApplication.Information;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.ViewModels.Result;

namespace BubblesDivePlanner.Controllers.Header
{
    public class NewApplicationStateController
    {
        public MainWindowViewModel NewApplication(MainWindowViewModel mainWindowViewModel)
        {
            var diveProfileService = new DiveProfileService();
            mainWindowViewModel.DiveApplication = new DiveApplicationViewModel(diveProfileService);
            mainWindowViewModel.DiveApplication.DivePlanSetup = new DivePlanSetupViewModel(diveProfileService);
            mainWindowViewModel.DiveApplication.DiveInformation = new DiveInformationViewModel();
            mainWindowViewModel.DiveApplication.DiveResults = new DiveResultsViewModel();

            return mainWindowViewModel;
        }
    }
}