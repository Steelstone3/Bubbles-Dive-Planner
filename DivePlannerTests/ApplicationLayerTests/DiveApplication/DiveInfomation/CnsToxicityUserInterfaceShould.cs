using DivePlannerMk3.Models;
using Xunit;

namespace DivePlannerTests
{
    public class CnsToxicityUserInterfaceShould
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