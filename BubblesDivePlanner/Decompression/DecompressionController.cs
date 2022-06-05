using System;
using System.Collections.Generic;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.Decompression
{
    public class DecompressionController
    {
        public Queue<IDiveStepModel> CollateDecompressionDiveSteps()
        {
            return null;
        }

        public IDiveStepModel CalculateDiveStepAtStepInterval()
        {
            return null;
        }

        public int FindNearestDepthToDiveCeiling(double diveCeiling)
        {
            const double stepInterval = 3;
            return (int)(Math.Ceiling(diveCeiling / stepInterval) * stepInterval);
        }
    }
}