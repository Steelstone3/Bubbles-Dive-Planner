using BubblesDivePlanner.Models.Information;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Models.Information
{
    public class CnsToxicityModelShould
    {
        [Fact]
        public void PopulateTheCnsToxicityModel()
        {
            var cnsToxicity = new CnsToxicityModel();

            Assert.NotNull(cnsToxicity.MaximumSingleDiveDuration);
            Assert.NotNull(cnsToxicity.OxygenPartialPressureConstant);
            Assert.NotNull(cnsToxicity.Total24HourDuration);
            
            Assert.NotEmpty(cnsToxicity.MaximumSingleDiveDuration);
            Assert.NotEmpty(cnsToxicity.OxygenPartialPressureConstant);
            Assert.NotEmpty(cnsToxicity.Total24HourDuration);
        }
    }
}