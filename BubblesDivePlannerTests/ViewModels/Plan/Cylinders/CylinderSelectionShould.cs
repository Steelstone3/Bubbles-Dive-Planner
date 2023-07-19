using System.Collections.Specialized;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using BubblesDivePlanner.ViewModels.Plan.Cylinders;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan.Cylinders
{
    public class CylindersShould
    {
        private readonly ICylinderSelection cylinderSelection = new CylinderSelection();

        [Fact]
        public void RaiseCollectionChanged()
        {
            // Given
            CylinderSelection cylinderSelectionVM = (CylinderSelection)cylinderSelection;
            Mock<ICylinder> cylinder = new();
            List<NotifyCollectionChangedAction> viewModelEvents = new();
            cylinderSelectionVM.Cylinders.CollectionChanged += (sender, e) => viewModelEvents.Add(e.Action);

            // When
            cylinderSelection.Cylinders.Add(cylinder.Object);

            // Then
            Assert.Contains(NotifyCollectionChangedAction.Add, viewModelEvents);
            Assert.Contains(cylinder.Object, cylinderSelectionVM.Cylinders);
        }
    }
}