using BubblesDivePlanner.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.DiveModels
{
    public class Zhl12BuhlmannModelShould
    {
        private readonly IDiveModel _diveModel = new Zhl12BuhlmannModel();

        [Fact]
        public void AllowModelToBeRead()
        {
            //Arrange
            const byte COMPARTMENT_COUNT = 16;
            double[] nitrogenHalfTime = new double[COMPARTMENT_COUNT] { 2.65, 7.94, 12.2, 18.5, 26.5, 37.0, 53.0, 79.0, 114.0, 146.0, 185.0, 238.0, 304.0, 397.0, 503.0, 635.0 };
            double[] heliumHalfTime = new double[COMPARTMENT_COUNT] { 1.0, 3.0, 4.6, 7.0, 10.0, 14.0, 20.0, 30.0, 43.0, 55.0, 70.0, 90.0, 115.0, 150.0, 190.0, 240.0 };
            // TODO Double check a b values
            double[] aValuesNitrogen = new double[COMPARTMENT_COUNT] { 2.2005, 1.5005, 1.0779, 0.9024, 0.7466, 0.5772, 0.4706, 0.4564, 0.4564, 0.4593, 0.4593, 0.3807, 0.2505, 0.2505, 0.2505, 0.2505 };
            double[] bValuesNitrogen = new double[COMPARTMENT_COUNT] { 0.82, 0.82, 0.825, 0.835, 0.845, 0.86, 0.87, 0.89, 0.89, 0.934, 0.934, 0.944, 0.962, 0.962, 0.962, 0.962 };
            double[] aValuesHelium = new double[COMPARTMENT_COUNT] { 2.2005, 1.5079, 1.0924, 0.9166, 0.7672, 0.5906, 0.4964, 0.4564, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001 };
            double[] bValuesHelium = new double[COMPARTMENT_COUNT] { 0.82, 0.825, 0.835, 0.845, 0.86, 0.87, 0.89, 0.89, 0.926, 0.926, 0.926, 0.926, 0.926, 0.926, 0.926, 0.926 };

            //Assert
            Assert.Equal("Zhl12 Model", _diveModel.DiveModelName);
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