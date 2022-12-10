using BubblesDivePlanner.Models;
using Xunit;

namespace Name
{
    public class DiveStepShould
    {
        [Theory]
        [InlineData(50, 50)]
        [InlineData(60, 60)]
        [InlineData(100, 100)]
        public void ContainsDepth(byte depth, byte expectedDepth)
        {
            // Given
            byte time = 10;

            // When
            IDiveStep diveStep = new DiveStep(depth, time);

            // Then
            Assert.Equal(expectedDepth, diveStep.Depth);
        }

        [Theory]
        [InlineData(50, 50)]
        [InlineData(60, 60)]
        public void ContainsTime(byte time, byte expectedTime)
        {
            // Given
            byte depth = 50;

            // When
            IDiveStep diveStep = new DiveStep(depth, time);

            // Then
            Assert.Equal(expectedTime, diveStep.Time);
        }
    }
}