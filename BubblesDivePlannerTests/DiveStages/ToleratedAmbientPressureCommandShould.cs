using System.Collections.Generic;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStages;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class ToleratedAmbientPressureShould
    {
        private IDiveModel _diveModel = new Zhl16BuhlmannModel();

        [Theory]
        [InlineData(
            new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
            new double[16] { 1.2559, 1.0000, 0.8618, 0.7562, 0.6667, 0.5600, 0.4947, 0.4500, 0.4187, 0.3798, 0.3497, 0.3223, 0.2850, 0.2737, 0.2523, 0.2327 },
            new double[16] { 0.5050, 0.6514, 0.7222, 0.7825, 0.8126, 0.8434, 0.8693, 0.8910, 0.9092, 0.9222, 0.9319, 0.9403, 0.9477, 0.9544, 0.9602, 0.9653 },
            new double[16] { -0.2353, -0.1368, -0.0519, 0.0264, 0.1002, 0.194, 0.2567, 0.3029, 0.3376, 0.3783, 0.4103, 0.4398, 0.4786, 0.4928, 0.5163, 0.538 })]
        public void RunToleratedAmbientPressureStage(double[] tissuePressuresTotal, double[] aValues,
            double[] bValues, double[] toleratedAmbientPressuresResult)
        {
            //Arrange
            _diveModel.DiveProfile = SetupDiveProfile(_diveModel.DiveProfile, tissuePressuresTotal, aValues, bValues);
            var diveStage = new ToleratedAmbientPressureCommand(_diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(toleratedAmbientPressuresResult, _diveModel.DiveProfile.ToleratedAmbientPressures);
        }

        private IDiveProfileModel SetupDiveProfile(IDiveProfileModel diveProfileModel, double[] tissuePressuresTotal, double[] aValues, double[] bValues)
        {
            diveProfileModel.TissuePressuresTotal = new List<double>(tissuePressuresTotal);
            diveProfileModel.AValues = new List<double>(aValues);
            diveProfileModel.BValues = new List<double>(bValues);

            return diveProfileModel;
        }
    }
}