using BubblesDivePlanner.DiveModels.DiveProfile;
using Xunit;

namespace BubblesDivePlannerTests.DiveModels.DiveProfile
{
    public class DiveProfileFactoryShould
    {
        [Theory]
        [InlineData(16)]
        [InlineData(12)]
        [InlineData(8)]
        public void SetDiveProfileToASize(int compartmentSize)
        {
            IDiveProfileFactory diveProfileFactory = new DiveProfileFactory();
            var diveProfileModel = diveProfileFactory.CreateDiveProfile(compartmentSize);

            Assert.Equal(compartmentSize, diveProfileModel.MaxSurfacePressures.Count);
            Assert.Equal(compartmentSize, diveProfileModel.ToleratedAmbientPressures.Count);
            Assert.Equal(compartmentSize, diveProfileModel.CompartmentLoad.Count);
            Assert.Equal(compartmentSize, diveProfileModel.TissuePressuresNitrogen.Count);
            Assert.Equal(compartmentSize, diveProfileModel.TissuePressuresHelium.Count);
            Assert.Equal(compartmentSize, diveProfileModel.TissuePressuresTotal.Count);
            Assert.Equal(compartmentSize, diveProfileModel.AValues.Count);
            Assert.Equal(compartmentSize, diveProfileModel.BValues.Count);
        }
    }
}