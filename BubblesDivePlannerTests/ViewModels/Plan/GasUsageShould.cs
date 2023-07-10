using System;
using BubblesDivePlanner.ViewModels.Models.Plan;
using BubblesDivePlanner.ViewModels.Plan;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan
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
    }
}