using Xunit;
using DivePlannerMk3.Models;
using DivePlannerMk3.Controllers.DiveStages;
using System.Collections.Generic;

namespace DivePlannerTests
{
    public class DiveProfileServiceTests
    {
        //Using the buhlmann model for tests
        private Zhl16Buhlmann _diveModel = new Zhl16Buhlmann();
        private DiveProfile _diveProfile = new DiveProfile();

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 1)]
        [InlineData(21, 0, 0, 0.21, 0, 0.79)]
        [InlineData(32, 50, 0, 0.32, 0.5, 0.18)]
        [InlineData(32, 50, 10, 0.64, 1, 0.36)]
        [InlineData(32, 50, 30, 1.28, 2, 0.72)]
        public void RunPreStageAmbientPressure(double oxygenPercentage, double heliumPercentage, int depth, double resultOxygen, double resultHelium, double resultNitrogen)
        {
            //Arrange
            var diveStage = new PreDiveStageAmbientPressure(_diveProfile, oxygenPercentage, heliumPercentage, depth);

            //Act
            diveStage.RunStage();

            //Assert
            Assert.Equal(resultOxygen, _diveProfile.PressureOxygen, 2);
            Assert.Equal(resultHelium, _diveProfile.PressureHelium, 2);
            Assert.Equal(resultNitrogen, _diveProfile.PressureNitrogen, 2);
        }

        [Theory]
        //Default a, b values of the bulhmann model expected
        [InlineData(new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
                    new double[16] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
                    new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
                    new double[16] { 1.2559, 1.0000, 0.8618, 0.7562, 0.6667, 0.5600, 0.4947, 0.4500, 0.4187, 0.3798, 0.3497, 0.3223, 0.2850, 0.2737, 0.2523, 0.2327 },
                    new double[16] { 0.5050, 0.6514, 0.7222, 0.7825, 0.8126, 0.8434, 0.8693, 0.8910, 0.9092, 0.9222, 0.9319, 0.9403, 0.9477, 0.9544, 0.9602, 0.9653 })]

        [InlineData(new double[16] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, },
                    new double[16] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, },
                    new double[16] { 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0 },
                    new double[16] { 1.4992, 1.1915, 1.0268, 0.901, 0.7944, 0.6902, 0.6126, 0.5501, 0.5068, 0.4672, 0.4415, 0.4206, 0.4015, 0.3956, 0.3848, 0.3723 },
                    new double[16] { 0.4648, 0.613, 0.6874, 0.7524, 0.7854, 0.8196, 0.8486, 0.8732, 0.8924, 0.9062, 0.9158, 0.9238, 0.93, 0.9358, 0.941, 0.946 })]
        public void RunStageABValues(double[] mockTissuePressureNitrogen, double[] mockTissuePressureHelium, double[] mockTissuePressureTotal, double[] aValueResult, double[] bValueResult)
        {
            //Arrange
            InitaliseDiveProfile();

            _diveProfile.TissuePressuresNitrogen = new List<double>(mockTissuePressureNitrogen);
            _diveProfile.TissuePressuresHelium = new List<double>(mockTissuePressureHelium);
            _diveProfile.TissuePressuresTotal = new List<double>(mockTissuePressureTotal);

            var diveStage = new DiveStageABValues(_diveModel, _diveProfile);

            //Act
            for (int i = 0; i < _diveModel.CompartmentCount; i++)
            {
                diveStage.RunStage();

                //Assert
                Assert.Equal(aValueResult[i], _diveProfile.AValues[i], 4);
                Assert.Equal(bValueResult[i], _diveProfile.BValues[i], 4);
            }
        }

        [Theory]
        [InlineData(new double[16] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
                    new double[16] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
                    new double[16] { 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0 })]
        public void RunStageCompartmentLoad(double[] mockTissuePressuresTotal, double[] mockMaxSurfacePressure, double[] compartmentLoadResult)
        {
            //Arrange
            InitaliseDiveProfile();

            _diveProfile.TissuePressuresTotal = new List<double>(mockTissuePressuresTotal);
            _diveProfile.MaxSurfacePressures = new List<double>(mockMaxSurfacePressure);

            var diveStage = new DiveStageCompartmentLoad(_diveModel, _diveProfile);

            //Act
            for (int i = 0; i < _diveModel.CompartmentCount; i++)
            {
                diveStage.RunStage();

                //Assert
                Assert.Equal(compartmentLoadResult[i], _diveProfile.CompartmentLoad[i], 4);
            }
        }

        [Theory]
        //Default a, b values
        [InlineData(new double[16] { 1.2559, 1.0000, 0.8618, 0.7562, 0.6667, 0.5600, 0.4947, 0.4500, 0.4187, 0.3798, 0.3497, 0.3223, 0.2850, 0.2737, 0.2523, 0.2327 },
                    new double[16] { 0.5050, 0.6514, 0.7222, 0.7825, 0.8126, 0.8434, 0.8693, 0.8910, 0.9092, 0.9222, 0.9319, 0.9403, 0.9477, 0.9544, 0.9602, 0.9653 },
                    new double[16] { 3.2361, 2.5352, 2.2465, 2.0342, 1.8973, 1.7457, 1.6451, 1.5723, 1.5186, 1.4642, 1.4228, 1.3858, 1.3402, 1.3215, 1.2937, 1.2686 })]
        public void RunStageMaximumSurfacePressure(double[] mockAValues, double[] mockBValues, double[] maxSurfacePressureResult)
        {
            //Arrange
            InitaliseDiveProfile();

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

        [Theory]
        //No bottom time, no change...
        [InlineData(new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
                    new double[16] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
                    new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
                    0,
                    new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
                    new double[16] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
                    new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 })]
        public void RunStageTissuePressure(double[] mockTissuePressuresNitrogen, double[] mockTissuePressuresHelium, double[] mockTissuePressuresTotal, int bottomTime, double[] tissuePressureNitrogenResult, double[] tissuePressureHeliumResult, double[] tissuePressureTotalResult)
        {
            InitaliseDiveProfile();

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

        [Theory]
        //Default values, with default expected
        [InlineData(new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
                    new double[16] { 1.2559, 1.0000, 0.8618, 0.7562, 0.6667, 0.5600, 0.4947, 0.4500, 0.4187, 0.3798, 0.3497, 0.3223, 0.2850, 0.2737, 0.2523, 0.2327 },
                    new double[16] { 0.5050, 0.6514, 0.7222, 0.7825, 0.8126, 0.8434, 0.8693, 0.8910, 0.9092, 0.9222, 0.9319, 0.9403, 0.9477, 0.9544, 0.9602, 0.9653 },
                    new double[16] { -0.2353, -0.1368, -0.0519, 0.0264, 0.1002, 0.194, 0.2567, 0.3029, 0.3376, 0.3783, 0.4103, 0.4398, 0.4786, 0.4928, 0.5163, 0.538 })]
        public void RunStageToleratedAmbientPressure(double[] mockTissuePressuresTotal, double[] mockAValues, double[] mockBValues, double[] toleratedAmbientPressuresResult)
        {
            InitaliseDiveProfile();

            _diveProfile.TissuePressuresTotal = new List<double>(mockTissuePressuresTotal);
            _diveProfile.AValues = new List<double>(mockAValues);
            _diveProfile.BValues = new List<double>(mockBValues);

            var diveStage = new DiveStageToleratedAmbientPressure(_diveModel.CompartmentCount, _diveProfile);

            //Act
            for (int i = 0; i < _diveModel.CompartmentCount; i++)
            {
                diveStage.RunStage();

                //Assert
                Assert.Equal(toleratedAmbientPressuresResult[i], _diveProfile.ToleratedAmbientPressures[i], 4);
            }
        }

        [Fact(Skip = "Big interegration test leaving for now")]
        public void RunDiveStages()
        {

        }

        private void InitaliseDiveProfile()
        {
            for (int i = 0; i < _diveModel.CompartmentCount; i++)
            {
                _diveProfile.MaxSurfacePressures.Add(0.0);
                _diveProfile.ToleratedAmbientPressures.Add(0.0);
                _diveProfile.CompartmentLoad.Add(0.0);

                _diveProfile.TissuePressuresNitrogen.Add(0.79);
                _diveProfile.TissuePressuresHelium.Add(0.0);
                _diveProfile.TissuePressuresTotal.Add(0.0);

                _diveProfile.AValues.Add(0.0);
                _diveProfile.BValues.Add(0.0);
            }
        }
    }
}
