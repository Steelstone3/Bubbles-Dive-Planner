using System.Collections.Generic;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.Results
{
    public class ResultViewModel : IResultModel
    {
        //TODO AH Raise Property Change?
        public IDiveStepModel DiveStepModel { get; set; }
        public IDiveProfileModel DiveProfileModel { get; set; }
    }
}