using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Model;
using BubblesDivePlanner.ViewModels.Model.Headers;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Plan.Information;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class MainWindowShould
    {
        private readonly IMainWindow mainWindow = new MainWindow();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.IsAssignableFrom<ViewModelBase>(mainWindow);
            Assert.NotNull(mainWindow.Header);
            Assert.NotNull(mainWindow.Planner);
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

            //Assert
            Assert.Contains(nameof(mainWindowVM.Header), viewModelEvents);
            Assert.Contains(nameof(mainWindowVM.Planner), viewModelEvents);
        }
    }
}