using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Models;
using BubblesDivePlanner.ViewModels.Models.DiveInformation;
using BubblesDivePlanner.ViewModels.Models.DivePlan;
using BubblesDivePlanner.ViewModels.Models.Header;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class MainWindowShould
    {
        private readonly IMainWindow mainWindow;

        public MainWindowShould()
        {
            Mock<IHeader> header = new();
            Mock<IPlanner> plan = new();
            Mock<IInformation> information = new();

            mainWindow = new MainWindow(header.Object, plan.Object, information.Object);
        }

        [Fact]
        public void Construct()
        {
            // Then
            Assert.NotNull(mainWindow.Header);
            Assert.NotNull(mainWindow.Plan);
            Assert.NotNull(mainWindow.Information);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            MainWindow mainWindowVM = (MainWindow)mainWindow;
            Mock<IPlanner> plan = new();
            Mock<IInformation> information = new();
            Mock<IHeader> header = new();
            List<string> viewModelEvents = new();
            mainWindowVM.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            mainWindowVM.Header = header.Object;
            mainWindowVM.Plan = plan.Object;
            mainWindowVM.Information = information.Object;

            //Assert
            Assert.Contains(nameof(mainWindowVM.Header), viewModelEvents);
            Assert.Contains(nameof(mainWindowVM.Plan), viewModelEvents);
            Assert.Contains(nameof(mainWindowVM.Information), viewModelEvents);
        }
    }
}