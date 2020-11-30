using System.Collections.Generic;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DiveInfo;
using Xunit;

namespace DivePlannerTests
{
    public class GasManagementInformationUiTests
    {
        //TODO (General) May need to test raise property changes occured in tests like this check example below

        InfoGasUsageViewModel _gasManagementInfo = new InfoGasUsageViewModel();

        [Theory]
        [InlineData(2000, 400)]
        [InlineData(500, 1000)]
        public void GasManagementModelCanBeSetTest(int gasRemaining, int gasUsedForStep)
        {
            //Arrange
            string gasRemainingEvent = "Not Fired";
            string gasUsedEvent = "Not Fired";

            //Act
            _gasManagementInfo.PropertyChanged += ((sender, e) => gasRemainingEvent = e.PropertyName);
            _gasManagementInfo.GasRemaining = gasRemaining;
            Assert.Equal(nameof(_gasManagementInfo.GasRemaining), gasRemainingEvent);
            
            _gasManagementInfo.PropertyChanged += ((sender, e) => gasUsedEvent = e.PropertyName);
            _gasManagementInfo.GasUsedForStep = gasUsedForStep;
            Assert.Equal(nameof(_gasManagementInfo.GasUsedForStep), gasUsedEvent);

            //Assert
            Assert.Equal(gasRemaining, _gasManagementInfo.GasRemaining);
            Assert.Equal(gasUsedForStep, _gasManagementInfo.GasUsedForStep);


        }

        [Fact]
        public void GasManagementVisiblityTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.False(_gasManagementInfo.UiEnabled);
        }
    }
}