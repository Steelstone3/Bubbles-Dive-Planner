using System.Collections.Generic;

namespace BubblesDivePlanner.ViewModels.Model.Plan.DiveModels
{
    public interface IDiveModel
    {
        string Name { get; }
        byte Compartments { get; }
        double[] NitrogenHalfTimes { get; }
        double[] HeliumHalfTimes { get; }
        double[] AValuesNitrogen { get; }
        double[] BValuesNitrogen { get; }
        double[] AValuesHelium { get; }
        double[] BValuesHelium { get; }
        IDiveProfile DiveProfile { get; }
    }
}