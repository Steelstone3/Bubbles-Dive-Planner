using System.Collections.Generic;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DiveResult;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveStageResults : IDiveStage
    {
        private DiveProfileResultsListViewModel _results;
        private IDiveProfile _diveProfile;
        
        public int Compartment 
        {
             get;set;
             }

        public DiveStageResults(DiveProfileResultsListViewModel results, IDiveProfile diveProfile)
        {
            _results = results;
            _diveProfile = diveProfile;
        }


        public void RunStage()
        {
            PopulateResults();
        }

        private void PopulateResults()
        {
            var stepResult = new DiveProfileStepOutputModel();
            //TODO AH fill in stepResult from dive profile
            //146 in index 0 the rest 0 why?!
            stepResult.CompartmentLoadResult = _diveProfile.CompartmentLoad[Compartment];
            _results.DiveProfileStepOutput.Add(stepResult);
        }
    }
}