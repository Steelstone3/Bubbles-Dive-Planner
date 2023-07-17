using System.Collections.Specialized;
using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;
using BubblesDivePlanner.ViewModels.Plan.DiveModels;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan.DiveModels
{
    public class DiveModelSelectionShould
    {
        private readonly IDiveModelSelection diveModelSelection = new DiveModelSelection();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.NotEmpty(diveModelSelection.DiveModels);
        }

        [Fact]
        public void RaiseCollectionChanged()
        {
            // Given
            Mock<IDiveModel> diveModel = new();
            DiveModelSelection diveModelSelectionVM = (DiveModelSelection)diveModelSelection;
            List<NotifyCollectionChangedAction> viewModelEvents = new();
            diveModelSelectionVM.DiveModels.CollectionChanged += (sender, e) => viewModelEvents.Add(e.Action);

            // When
            diveModelSelection.DiveModels.Add(diveModel.Object);

            // Then
            Assert.Contains(NotifyCollectionChangedAction.Add, viewModelEvents);
            Assert.Contains(diveModel.Object, diveModelSelectionVM.DiveModels);
        }
    }
}