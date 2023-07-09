using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Models;
using BubblesDivePlanner.ViewModels.Models.DiveInformation;
using BubblesDivePlanner.ViewModels.Models.DivePlan;
using BubblesDivePlanner.ViewModels.Models.Header;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class MainWindowViewModelShould
    {
        private readonly IMainWindowViewModel mainWindowViewModel;

        public MainWindowViewModelShould()
        {
            Mock<IHeaderModel> headerModel = new();
            Mock<IDivePlanModel> divePlanModel = new();
            Mock<IDiveInformationModel> diveInformationModel = new();

            mainWindowViewModel = new MainWindowViewModel(headerModel.Object, divePlanModel.Object, diveInformationModel.Object);
        }

        [Fact]
        public void Construct()
        {
            // Then
            Assert.NotNull(mainWindowViewModel.Header);
            Assert.NotNull(mainWindowViewModel.DivePlan);
            Assert.NotNull(mainWindowViewModel.DiveInformation);
        }
    }
}