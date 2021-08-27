using System.Collections.Generic;
using BubblesDivePlanner.Commands.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages.Helpers;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages
{
    public class TissuePressureShould
    {
        //Using the buhlmann model for tests
        private Zhl16Buhlmann _diveModel = new Zhl16Buhlmann();
        private DiveProfile _diveProfile = new DiveProfile();

        [Theory]
        //No bottom time, no change...
        [InlineData(
            new double[16]
                { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
            new double[16] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
            new double[16]
                { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
            0,
            new double[16]
                { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
            new double[16] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
            new double[16]
                { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 })]
        public void RunTissuePressureStage(double[] mockTissuePressuresNitrogen, double[] mockTissuePressuresHelium,
            double[] mockTissuePressuresTotal, int bottomTime, double[] tissuePressureNitrogenResult,
            double[] tissuePressureHeliumResult, double[] tissuePressureTotalResult)
        {
            DiveProfileHelper.InitaliseDiveProfile();

            _diveProfile.TissuePressuresNitrogen = new List<double>(mockTissuePressuresNitrogen);
            _diveProfile.TissuePressuresHelium = new List<double>(mockTissuePressuresHelium);
            _diveProfile.TissuePressuresTotal = new List<double>(mockTissuePressuresTotal);

            var diveStage = new DiveStageTissuePressure(_diveModel, _diveProfile, bottomTime);


            //Act
            for (int i = 0; i < _diveModel.CompartmentCount; i++)
            {
                diveStage.RunStage();

                //Assert
                Assert.Equal(tissuePressureNitrogenResult[i], _diveProfile.TissuePressuresNitrogen[i], 2);
                Assert.Equal(tissuePressureHeliumResult[i], _diveProfile.TissuePressuresHelium[i], 2);
                Assert.Equal(tissuePressureTotalResult[i], _diveProfile.TissuePressuresTotal[i], 2);
            }
        }
    }
}