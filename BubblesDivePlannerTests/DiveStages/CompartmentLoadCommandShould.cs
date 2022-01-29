using System.Collections.Generic;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStages;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class CompartmentLoadCommandShould
    {
        private IDiveModel _diveModel = new Zhl16BuhlmannModel();

        [Theory]
        [InlineData(new double[16] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
            new double[16] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
            new double[16] { 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0 })]
        public void RunCompartmentLoadStage(double[] tissuePressuresTotal, double[] maxSurfacePressure,
            double[] compartmentLoadResult)
        {
            //Arrange
            SetupDiveStage(tissuePressuresTotal, maxSurfacePressure);
            var diveStage = new CompartmentLoadCommand(_diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(compartmentLoadResult, _diveModel.DiveProfile.CompartmentLoad);
        }

        private void SetupDiveStage(double[] tissuePressuresTotal, double[] maxSurfacePressure)
        {
            _diveModel.DiveProfile.TissuePressuresTotal = new List<double>(tissuePressuresTotal);
            _diveModel.DiveProfile.MaxSurfacePressures = new List<double>(maxSurfacePressure);
        }
    }
}