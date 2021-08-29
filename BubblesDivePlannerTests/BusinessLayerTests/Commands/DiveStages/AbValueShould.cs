using System.Collections.Generic;
using BubblesDivePlanner.Commands.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages.Helpers;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages
{
    public class AbValueShould
    {
        //Using the buhlmann model for tests
        private Zhl16Buhlmann _diveModel = new Zhl16Buhlmann();
        private DiveProfile _diveProfile = new DiveProfile();

        [Theory]
        //Default a, b values of the bulhmann model expected
        [InlineData(
            new double[16]
                { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
            new double[16] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
            new double[16]
                { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
            new double[16]
            {
                1.2559, 1.0000, 0.8618, 0.7562, 0.6667, 0.5600, 0.4947, 0.4500, 0.4187, 0.3798, 0.3497, 0.3223,
                0.2850, 0.2737, 0.2523, 0.2327
            },
            new double[16]
            {
                0.5050, 0.6514, 0.7222, 0.7825, 0.8126, 0.8434, 0.8693, 0.8910, 0.9092, 0.9222, 0.9319, 0.9403,
                0.9477, 0.9544, 0.9602, 0.9653
            })]
        [InlineData(
            new double[16] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, },
            new double[16] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, },
            new double[16] { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
            new double[16]
            {
                1.4992, 1.1915, 1.0268, 0.901, 0.7944, 0.6902, 0.6126, 0.5501, 0.5068, 0.4672, 0.4415, 0.4206,
                0.4015, 0.3956, 0.3848, 0.3723
            },
            new double[16]
            {
                0.4648, 0.613, 0.6874, 0.7524, 0.7854, 0.8196, 0.8486, 0.8732, 0.8924, 0.9062, 0.9158, 0.9238, 0.93,
                0.9358, 0.941, 0.946
            })]
        public void RunAbValuesStage(double[] mockTissuePressureNitrogen, double[] mockTissuePressureHelium,
            double[] mockTissuePressureTotal, double[] aValueResult, double[] bValueResult)
        {
            //Arrange
            _diveProfile = DiveProfileHelper.InitaliseDiveProfile();

            _diveProfile.TissuePressuresNitrogen = new List<double>(mockTissuePressureNitrogen);
            _diveProfile.TissuePressuresHelium = new List<double>(mockTissuePressureHelium);
            _diveProfile.TissuePressuresTotal = new List<double>(mockTissuePressureTotal);

            var diveStage = new DiveStageAbValues(_diveModel, _diveProfile);

            //Act
            for (int i = 0; i < _diveModel.CompartmentCount; i++)
            {
                diveStage.RunStage();

                //Assert
                Assert.Equal(aValueResult[i], _diveProfile.AValues[i], 4);
                Assert.Equal(bValueResult[i], _diveProfile.BValues[i], 4);
            }
        }
    }
}