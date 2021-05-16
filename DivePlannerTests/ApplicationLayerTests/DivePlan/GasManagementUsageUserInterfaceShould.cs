using System.Collections.Generic;
using DivePlannerMk3.ViewModels.DivePlan;
using Xunit;

namespace DivePlannerTests
{
    public class GasManagementUsageUserInterfaceShould
    {
        private GasManagementViewModel _gasManagement = new GasManagementViewModel();
        
        [Fact]
        public void RaisePropertyChangedWhenInitialGasVolumeIsSet()
        {
            string initialGasEvent = "Not Fired";

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => initialGasEvent = e.PropertyName);
            _gasManagement.InitialCylinderTotalVolume = 4000;
            Assert.Equal(nameof(_gasManagement.InitialCylinderTotalVolume), initialGasEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenGasUsedVolumeIsSet()
        {
            var gasUsedEvents = new List<string>();

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => gasUsedEvents.Add(e.PropertyName));
            _gasManagement.GasUsedForStep = 200;
            Assert.Equal(nameof(_gasManagement.GasUsedForStep), gasUsedEvents[0]);
            Assert.Equal(nameof(_gasManagement.GasRemaining), gasUsedEvents[1]);
        }

        [Fact]
        public void RaisePropertyChangedWhenGasRemainingVolumeIsSet()
        {
            string gasRemainingEvent = "Not Fired";

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => gasRemainingEvent = e.PropertyName);
            _gasManagement.GasRemaining = 2000;
            Assert.Equal(nameof(_gasManagement.GasRemaining), gasRemainingEvent);
        }

        [Fact(Skip = "Test needs Implementing")]
        public void HaveAPostDiveStepVisibiltyState()
        {
            //Arrange

            //Act

            //Assert
            Assert.True(_gasManagement.IsGasUsageVisible);
            Assert.False(_gasManagement.IsUiVisible);
            Assert.False(_gasManagement.IsUiEnabled);
        }
    }
}