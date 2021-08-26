using System.Collections.Generic;

namespace BubblesDivePlanner.Contracts.Models.DiveModels
{
    public interface IDiveModel
    {
        int CompartmentCount
        {
            get; set;
        }

        string DiveModelName
        {
            get; set;
        }

        List<double> NitrogenHalfTime
        {
            get; set;
        }

        List<double> HeliumHalfTime
        {
            get; set;
        }

        List<double> AValuesNitrogen
        {
            get; set;
        }

        List<double> BValuesNitrogen
        {
            get; set;
        }

        List<double> AValuesHelium
        {
            get; set;
        }

        List<double> BValuesHelium
        {
            get; set;
        }
    }
}