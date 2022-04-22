using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlanner.Results
{
    public interface IResultModel
    {
        IDiveStepModel DiveStepModel { get; set; }
        IDiveProfileModel DiveProfileModel { get; set; }
        IResultsHistoryModel ResultsHistoryModel { get; }
    }
}