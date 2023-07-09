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
            Assert.NotNull(mainWindowViewModel.Plan);
            Assert.NotNull(mainWindowViewModel.Information);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            MainWindowViewModel mainWindow = (MainWindowViewModel)mainWindowViewModel;
            Mock<IDivePlanModel> plan = new();
            Mock<IDiveInformationModel> information = new();
            Mock<IHeaderModel> header = new();
            List<string> viewModelEvents = new();
            mainWindow.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            mainWindow.Header = header.Object;
            mainWindow.Plan = plan.Object;
            mainWindow.Information = information.Object;

            //Assert
            Assert.Contains(nameof(mainWindow.Header), viewModelEvents);
            Assert.Contains(nameof(mainWindow.Plan), viewModelEvents);
            Assert.Contains(nameof(mainWindow.Information), viewModelEvents);
        }
    }
}