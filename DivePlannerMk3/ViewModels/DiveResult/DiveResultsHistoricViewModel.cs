using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.ViewModels.DiveResult
{
    public class HistoricDiveResultsViewModel : IDiveResultsHistoricModel
    {
        public ObservableCollection<IDiveResultsModel> DiveProfileResults
        {
            get;
        } = new ObservableCollection<IDiveResultsModel>();
        
        public List<IDiveResultsModel> _newDiveProfileStepResults;
        public List<IDiveResultsModel> NewDiveProfileStepResults 
        { 
            get => _newDiveProfileStepResults;
            set
            {
                _newDiveProfileStepResults = value;
                ConvertToObservable(_newDiveProfileStepResults);
            }
        }

        private void ConvertToObservable(List<IDiveResultsModel> newDiveProfileStepResults)
        {
            foreach(var result in newDiveProfileStepResults)
            {
                DiveProfileResults.Add(result);
            }
        }
    }
}

