using System.Collections.Specialized;
using System.Reactive;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using BubblesDivePlanner.ViewModels.Plan.Cylinders;
using Moq;
using ReactiveUI;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan.Cylinders
{
    public class CylindersShould
    {
        private readonly ICylinderSelection cylinderSelection = new CylinderSelection();

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            CylinderSelection cylinderSelectionVM = (CylinderSelection)cylinderSelection;
            Mock<ICylinder> cylinder = new();
            Mock<IGasMixture> gasMixture = new();
            Mock<IGasUsage> gasUsage = new();
            List<string> viewModelEvents = new();
            cylinderSelectionVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            cylinderSelectionVM.Cylinder = cylinder.Object;

            //Assert
            Assert.Contains(nameof(cylinderSelectionVM.Cylinder), viewModelEvents);
        }

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

        [Fact]
        public void AddCylinder()
        {
            // Given
            CylinderSelection cylinderSelection = new();
            ReactiveCommand<Unit, Unit> addCylinderCommand = ReactiveCommand.Create(cylinderSelection.AddCylinder);

            // When
            addCylinderCommand.Execute().Subscribe();
            addCylinderCommand.Execute().Subscribe();

            // Then
            Assert.NotEmpty(cylinderSelection.Cylinders);
            Assert.NotSame(cylinderSelection.Cylinders[0], cylinderSelection.Cylinders[1]);
        }
    }
}