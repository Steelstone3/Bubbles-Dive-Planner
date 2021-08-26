using System.Collections.Generic;
using BubblesDivePlanner.Contracts.Models.Results;

namespace BubblesDivePlanner.Models.Results
{
    public class DiveResultsModel
    {
        public List<IDiveProfileStepOutputModel> DiveProfileStepOutput
        {
            get;
        } = new List<IDiveProfileStepOutputModel>();
    }
}
