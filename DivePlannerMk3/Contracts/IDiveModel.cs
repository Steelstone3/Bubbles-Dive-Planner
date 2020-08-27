using System.Collections.Generic;

namespace DivePlannerMk3.Contracts
{
    public interface IDiveModel
    {
        int CompartmentIndexMax
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