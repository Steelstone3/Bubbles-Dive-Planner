using System.Collections.Generic;
using BubblesDivePlanner.Commands.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages.Helpers;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages
{
    public class CompartmentLoadShould
    {
        //Using the buhlmann model for tests
        private Zhl16Buhlmann _diveModel = new Zhl16Buhlmann();
        private DiveProfile _diveProfile = new DiveProfile();

        [Theory]
        [InlineData(new double[16] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
            new double[16] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
            new double[16]
            {
                100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0, 100.0,
                100.0
            })]
        public void RunCompartmentLoadStage(double[] mockTissuePressuresTotal, double[] mockMaxSurfacePressure,
            double[] compartmentLoadResult)
        {
            //Arrange
            _diveProfile = DiveProfileHelper.InitaliseDiveProfile();

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
    }
}