using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Planner.Cylinders;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan.Cylinders
{
    public class GasUsageShould
    {
        private readonly IGasUsage gasUsage = new GasUsage();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.Equal(0u, gasUsage.GasRemaining);
            Assert.Equal(0u, gasUsage.GasUsed);
            Assert.Equal(0u, gasUsage.SurfaceAirConsumptionRate);
        }

        [Fact]
        public void DeriveFrom()
        {
            // Then
            Assert.IsAssignableFrom<ViewModelBase>(gasUsage);
            Assert.IsAssignableFrom<IGasUsage>(gasUsage);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            GasUsage gasUsageVM = (GasUsage)gasUsage;
            List<string> viewModelEvents = new();
            gasUsageVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            gasUsageVM.GasRemaining = 2000;
            gasUsageVM.GasUsed = 400;
            gasUsageVM.SurfaceAirConsumptionRate = 12;

            //Assert
            Assert.Contains(nameof(gasUsageVM.GasRemaining), viewModelEvents);
            Assert.Contains(nameof(gasUsageVM.GasUsed), viewModelEvents);
            Assert.Contains(nameof(gasUsageVM.SurfaceAirConsumptionRate), viewModelEvents);
        }

        [Fact]
        public void IsVisible()
        {
            // Given
            GasUsage gasUsage = new();

            // Then
            Assert.False(gasUsage.IsVisible);
        }
    }
}