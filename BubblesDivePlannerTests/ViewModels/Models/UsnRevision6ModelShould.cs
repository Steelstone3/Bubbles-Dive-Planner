using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.ViewModels.Models;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Models
{
    public class UsnRevision6ModelShould
    {
        private readonly IDiveModel _diveModel = new UsnRevision6Model();

        [Fact]
        public void AllowModelToBeRead()
        {
            //Arrange
            const byte COMPARTMENT_COUNT = 9;
            double[] nitrogenHalfTime = new double[COMPARTMENT_COUNT] { 5.0, 10.0, 20.0, 40.0, 80.0, 120.0, 160.0, 200.0, 240.0 };
            double[] heliumHalfTime = new double[COMPARTMENT_COUNT] { 5.0, 10.0, 20.0, 40.0, 80.0, 120.0, 160.0, 200.0, 240.0 };
            double[] aValuesNitrogen = new double[COMPARTMENT_COUNT] { 1.37, 1.08, 0.69, 0.3, 0.34, 0.38, 0.4, 0.45, 0.42 };
            double[] bValuesNitrogen = new double[COMPARTMENT_COUNT] { 0.555, 0.625, 0.666, 0.714, 0.769, 0.833, 0.870, 0.909, 0.909 };
            double[] aValuesHelium = new double[COMPARTMENT_COUNT] { 1.12, 0.85, 0.71, 0.63, 0.5, 0.44, 0.54, 0.61, 0.61 };
            double[] bValuesHelium = new double[COMPARTMENT_COUNT] { 0.67, 0.714, 0.769, 0.83, 0.83, 0.91, 1.0, 1.0, 1.0 };

            //Assert
            Assert.Equal("USN Revision 6 Model", _diveModel.DiveModelName);
            Assert.Equal(COMPARTMENT_COUNT, _diveModel.CompartmentCount);
            Assert.Equal(nitrogenHalfTime, _diveModel.NitrogenHalfTime);
            Assert.Equal(heliumHalfTime, _diveModel.HeliumHalfTime);
            Assert.Equal(aValuesNitrogen, _diveModel.AValuesNitrogen);
            Assert.Equal(bValuesNitrogen, _diveModel.BValuesNitrogen);
            Assert.Equal(aValuesHelium, _diveModel.AValuesHelium);
            Assert.Equal(bValuesHelium, _diveModel.BValuesHelium);
            Assert.NotNull(_diveModel.DiveProfile);
        }
    }
}