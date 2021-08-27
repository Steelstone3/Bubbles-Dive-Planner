using System.Collections.Generic;
using BubblesDivePlanner.Commands.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages.Helpers;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages
{
    public class MaximumSurfacePressureShould
    {
        //Using the buhlmann model for tests
        private Zhl16Buhlmann _diveModel = new Zhl16Buhlmann();
        private DiveProfile _diveProfile = new DiveProfile();

        [Theory]
        //Default a, b values
        [InlineData(
            new double[16]
            {
                1.2559, 1.0000, 0.8618, 0.7562, 0.6667, 0.5600, 0.4947, 0.4500, 0.4187, 0.3798, 0.3497, 0.3223, 0.2850,
                0.2737, 0.2523, 0.2327
            },
            new double[16]
            {
                0.5050, 0.6514, 0.7222, 0.7825, 0.8126, 0.8434, 0.8693, 0.8910, 0.9092, 0.9222, 0.9319, 0.9403, 0.9477,
                0.9544, 0.9602, 0.9653
            },
            new double[16]
            {
                3.2361, 2.5352, 2.2465, 2.0342, 1.8973, 1.7457, 1.6451, 1.5723, 1.5186, 1.4642, 1.4228, 1.3858, 1.3402,
                1.3215, 1.2937, 1.2686
            })]
        public void RunMaximumSurfacePressureStage(double[] mockAValues, double[] mockBValues,
            double[] maxSurfacePressureResult)
        {
            //Arrange
            _diveProfile = DiveProfileHelper.InitaliseDiveProfile();

            _diveProfile.AValues = new List<double>(mockAValues);
            _diveProfile.BValues = new List<double>(mockBValues);

            var diveStage = new DiveStageMaximumSurfacePressure(_diveModel.CompartmentCount, _diveProfile);

            //Act
            for (int i = 0; i < _diveModel.CompartmentCount; i++)
            {
                diveStage.RunStage();

                //Assert
                Assert.Equal(maxSurfacePressureResult[i], _diveProfile.MaxSurfacePressures[i], 4);
            }
        }
    }
}