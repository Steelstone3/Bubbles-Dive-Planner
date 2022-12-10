namespace BubblesDivePlanner.Models.DiveModels
{
    public interface IDiveModel
    {
        string Name { get; }
        byte CompartmentCount { get; }
        double[] NitrogenHalfTimes { get; }
        double[] HeliumHalfTimes { get; }
        double[] AValuesNitrogen { get; }
        double[] BValuesNitrogen { get; }
        double[] AValuesHelium { get; }
        double[] BValuesHelium { get; }
        IDiveProfile DiveProfile { get; }
    }
}