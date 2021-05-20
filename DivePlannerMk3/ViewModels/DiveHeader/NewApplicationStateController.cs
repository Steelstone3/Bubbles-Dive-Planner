using DivePlannerMk3.Controllers;
using DivePlannerMk3.ViewModels.DiveInfo;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;

namespace DivePlannerMk3.ViewModels.DiveHeader
{
    public class NewApplicationStateController
    {
        public MainWindowViewModel NewApplication(MainWindowViewModel mainWindowViewModel)
        {
            mainWindowViewModel.DiveResults = new DiveResultsViewModel();
            mainWindowViewModel.DivePlan = new DivePlanViewModel(new DiveProfileService());
            mainWindowViewModel.DiveInfo = new DiveInfoViewModel();

            return mainWindowViewModel;
        }
    }
}