using System.Collections.Generic;
using BubblesDivePlanner.DiveModels.DiveProfile;

namespace BubblesDivePlanner.DiveModels
{
    public interface IDiveModel
    {
        string DiveModelName { get; }
        int CompartmentCount { get; }
        IList<double> NitrogenHalfTime { get; }
        IList<double> HeliumHalfTime { get; }
        IList<double> AValuesNitrogen { get; }
        IList<double> BValuesNitrogen { get; }
        IList<double> AValuesHelium { get; }
        IList<double> BValuesHelium { get; }
        IDiveProfileModel DiveProfile { get; set; }
    }
}