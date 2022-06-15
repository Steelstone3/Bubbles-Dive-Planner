using Xunit;
using BubblesDivePlanner.Visibility;
using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlannerTests.TestFixtures;

namespace BubblesDivePlannerTests.Visibility
{
    public class VisibilityControllerShould
    {
        [Fact]
        public void HideViews()
        {
            //Arrange
            IMainWindowModel mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.DivePlanner.DiveModelSelector.SelectedDiveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            mainWindowViewModel.DivePlanner.CylinderSelector.Cylinders.Add(DivePlannerApplicationTestFixture.GetSelectedCylinder);
            mainWindowViewModel.DivePlanner.CylinderSelector.Cylinders.Add(DivePlannerApplicationTestFixture.GetSelectedCylinder);

            //Act
            VisibilityController.UpdateVisibilty(mainWindowViewModel);

            //Assert
            foreach (var cylinder in mainWindowViewModel.DivePlanner.CylinderSelector.Cylinders)
            {
                Assert.True(cylinder.GasUsage.IsVisible);
                Assert.False(cylinder.GasMixture.IsVisible);
                Assert.False(cylinder.IsVisible);
            }

            Assert.False(mainWindowViewModel.DivePlanner.DiveModelSelector.IsVisible);            
        }
    }
}