using DivePlannerMk3.Contracts;
using DivePlannerMk3.ViewModels;
using DivePlannerMk3.ViewModels.DiveHeader;
using DivePlannerMk3.ViewModels.DiveInfo;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;
using Moq;
using Xunit;

namespace DivePlannerTests
{
    public class NewDiveBusinessLayerShould
    {
        [Fact]
        public void ResetAllDiveApplicationModels()
        {
            var mainWindowViewModelOriginal = new MainWindowViewModel()
            {
                DiveHeader = new DiveHeaderViewModel(),
                DivePlan = new DivePlanViewModel(new Mock<IDiveProfileService>().Object)
                {
                    DiveStep = new DiveStepViewModel()
                    {
                        Depth = 50,
                        Time = 10,
                    },
                },
                DiveInfo = new DiveInfoViewModel()
                {
                    DiveCeilingViewModel = new DiveCeilingViewModel()
                    {
                        DiveCeiling = 12,
                    }
                },
                DiveResults = new DiveResultsViewModel()
                {
                    DiveParametersResult = new DiveParametersResultViewModel()
                    {
                        Depth = 50,
                        Time = 10,
                        DiveModelUsed = "Bob",
                        DiveProfileStepHeader = "Dive Step",
                    }
                },
            };

            var newApplicationState = new NewApplicationStateController();
            var mainWindowViewModel = newApplicationState.NewApplication(mainWindowViewModelOriginal);

            Assert.Equal(mainWindowViewModelOriginal.DiveHeader, mainWindowViewModel.DiveHeader);

            Assert.NotEqual(50, mainWindowViewModelOriginal.DivePlan.DiveStep.Depth);
            Assert.NotEqual(10, mainWindowViewModelOriginal.DivePlan.DiveStep.Time);

            Assert.NotEqual(12, mainWindowViewModelOriginal.DiveInfo.DiveCeilingViewModel.DiveCeiling);
            
            Assert.NotEqual(50, mainWindowViewModelOriginal.DiveResults.DiveParametersResult.Depth);
            Assert.NotEqual(10, mainWindowViewModelOriginal.DiveResults.DiveParametersResult.Time);
            Assert.NotEqual("Bob", mainWindowViewModelOriginal.DiveResults.DiveParametersResult.DiveModelUsed);
            Assert.NotEqual("Dive Step", mainWindowViewModelOriginal.DiveResults.DiveParametersResult.DiveProfileStepHeader);
        }
    }
}