using System.Collections.Specialized;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Models.Plan;
using BubblesDivePlanner.ViewModels.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan
{
    public class CylindersShould
    {
        private readonly ICylinderSelection cylinderSelection = new CylinderSelection();

        [Fact]
        public void RaiseCollectionChanged()
        {
            // Given
            Mock<ICylinder> cylinder = new();
            CylinderSelection cylinderSelectionVM = (CylinderSelection)cylinderSelection;
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