using System.Collections.Generic;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStages;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class MaximumSurfacePressureCommandShould
    {
        private IDiveModel _diveModel = new Zhl16BuhlmannModel();

        [Theory]
        [InlineData(
            new double[16] { 1.2559, 1.0000, 0.8618, 0.7562, 0.6667, 0.5600, 0.4947, 0.4500, 0.4187, 0.3798, 0.3497, 0.3223, 0.2850, 0.2737, 0.2523, 0.2327 },
            new double[16] { 0.5050, 0.6514, 0.7222, 0.7825, 0.8126, 0.8434, 0.8693, 0.8910, 0.9092, 0.9222, 0.9319, 0.9403, 0.9477, 0.9544, 0.9602, 0.9653 },
            new double[16] { 3.2361, 2.5352, 2.2465, 2.0342, 1.8973, 1.7457, 1.6451, 1.5723, 1.5186, 1.4642, 1.4228, 1.3858, 1.3402, 1.3215, 1.2937, 1.2686 })]
        public void RunMaximumSurfacePressureStage(double[] aValues, double[] bValues,
            double[] maxSurfacePressureResult)
        {
            //Arrange
            SetupDiveStage(aValues, bValues);

            var diveStage = new MaximumSurfacePressureCommand(_diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(maxSurfacePressureResult, _diveModel.DiveProfile.MaxSurfacePressures);
        }

        private void SetupDiveStage(double[] aValues, double[] bValues)
        {
            _diveModel.DiveProfile.AValues = new List<double>(aValues);
            _diveModel.DiveProfile.BValues = new List<double>(bValues);
        }
    }
}