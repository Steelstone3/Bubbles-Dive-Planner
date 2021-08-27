using System.Collections.Generic;
using BubblesDivePlanner.Contracts.Models.Results;

namespace BubblesDivePlanner.Models.Results
{
    public class DiveResultsStepOutputModel : IDiveResultsStepOutputModel
    {
        public List<IDiveProfileResultModel> DiveProfileStepOutput
        {
            get;
        } = new List<IDiveProfileResultModel>();
    }
}
