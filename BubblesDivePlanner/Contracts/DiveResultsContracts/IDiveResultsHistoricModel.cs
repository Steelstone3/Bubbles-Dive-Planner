using System.Collections.Generic;
using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Contracts
{
    public interface IDiveResultsHistoricModel
    {
        List<IDiveResultsModel> NewDiveProfileStepResults
        {
            get; set;
        }
    }
}