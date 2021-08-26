using System.Collections.Generic;

namespace BubblesDivePlanner.Contracts.Models.Results
{
    public interface IDiveResultsHistoricModel
    {
        List<IDiveResultsModel> NewDiveProfileStepResults
        {
            get; set;
        }
    }
}