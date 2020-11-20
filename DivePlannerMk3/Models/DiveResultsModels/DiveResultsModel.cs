using System.Collections.Generic;
using DivePlannerMk3.Contracts;


namespace DivePlannerMk3.Models
{
    public class DiveResultsModel
    {
        public List<IDiveProfileStepOutputModel> DiveProfileStepOutput
        {
            get;
        } = new List<IDiveProfileStepOutputModel>();

        public DiveParametersOutputModel DiveParametersOutput
        {
            get; set;
        } = new DiveParametersOutputModel();
    }
}
