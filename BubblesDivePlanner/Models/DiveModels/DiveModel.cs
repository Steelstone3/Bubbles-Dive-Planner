using System.Collections.Generic;
using BubblesDivePlanner.Contracts.Models.DiveModels;

namespace BubblesDivePlanner.Models.DiveModels
{
    public class DiveModel : IDiveModel
    {
        public int CompartmentCount
        {
            get; set;
        }

        public string DiveModelName
        {
            get; set;
        }

        public List<double> NitrogenHalfTime
        {
            get; set;
        }

        public List<double> HeliumHalfTime
        {
            get; set;
        }

        public List<double> AValuesNitrogen
        {
            get; set;
        }

        public List<double> BValuesNitrogen
        {
            get; set;
        }

        public List<double> AValuesHelium
        {
            get; set;
        }

        public List<double> BValuesHelium
        {
            get; set;
        }
    }
}