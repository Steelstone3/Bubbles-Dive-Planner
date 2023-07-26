using BubblesDivePlanner.Controllers.Prototypes;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Planner.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.Prototypes
{
    public class DiveProfilePrototypeShould
    {
        [Fact]
        public void DeepClone()
        {
            // Given
            DiveProfile diveProfile = new(16);

            // When
            IDiveProfile deepClone = DiveProfilePrototype.DeepClone(diveProfile);

            // Then
            Assert.Equivalent(diveProfile, deepClone);
            Assert.NotSame(diveProfile, deepClone);
        }
    }
}