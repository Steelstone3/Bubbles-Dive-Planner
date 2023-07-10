using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Model;
using BubblesDivePlanner.ViewModels.Model.Headers;
using BubblesDivePlanner.ViewModels.Model.Information;
using BubblesDivePlanner.ViewModels.Model.Plan;
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
            Mock<IPlanner> planner = new();
            Mock<IDiveInformation> information = new();

            mainWindow = new MainWindow(header.Object, planner.Object, information.Object);
        }

        [Fact]
        public void Construct()
        {
            // Then
            Assert.NotNull(mainWindow.Header);
            Assert.NotNull(mainWindow.Planner);
            Assert.NotNull(mainWindow.Information);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            MainWindow mainWindowVM = (MainWindow)mainWindow;
            Mock<IPlanner> planner = new();
            Mock<IDiveInformation> information = new();
            Mock<IHeader> header = new();
            List<string> viewModelEvents = new();
            mainWindowVM.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            mainWindowVM.Header = header.Object;
            mainWindowVM.Planner = planner.Object;
            mainWindowVM.Information = information.Object;

            //Assert
            Assert.Contains(nameof(mainWindowVM.Header), viewModelEvents);
            Assert.Contains(nameof(mainWindowVM.Planner), viewModelEvents);
            Assert.Contains(nameof(mainWindowVM.Information), viewModelEvents);
        }
    }
}