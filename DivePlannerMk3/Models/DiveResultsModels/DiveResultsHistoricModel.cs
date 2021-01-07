using System.Collections.Generic;
using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Models
{
    public class DiveResultsHistoricModel : IDiveResultsHistoricModel
    {
        public List<IDiveResultsModel> NewDiveProfileStepResults
        {
            get; set;
        } = new List<IDiveResultsModel>();
    }
}