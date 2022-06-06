using System.Collections.Generic;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Visibility;

namespace BubblesDivePlanner.DecompressionProfile
{
    public interface IDecompressionProfileModel
    {
        IList<IDiveStepModel> DecompressionDiveSteps { get; set; }
    }
}