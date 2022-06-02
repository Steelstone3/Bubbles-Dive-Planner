using System.Collections.Generic;
using BubblesDivePlanner.DiveModels.DiveProfile;

namespace BubblesDivePlanner.DiveModels
{
    public interface IDiveModel
    {
        string DiveModelName { get; }
        int CompartmentCount { get; }
        double[] NitrogenHalfTime { get; }
        double[] HeliumHalfTime { get; }
        double[] AValuesNitrogen { get; }
        double[] BValuesNitrogen { get; }
        double[] AValuesHelium { get; }
        double[] BValuesHelium { get; }
        IDiveProfileModel DiveProfile { get; set; }
    }
}