namespace BubblesDivePlanner.ViewModels.Model.Plan.DiveModels
{
    public interface IDiveModel
    {
        string Name { get; }
        byte Compartments { get; }
        float[] NitrogenHalfTimes { get; }
        float[] HeliumHalfTimes { get; }
        float[] AValuesNitrogen { get; }
        float[] BValuesNitrogen { get; }
        float[] AValuesHelium { get; }
        float[] BValuesHelium { get; }
        IDiveProfile DiveProfile { get; }
    }
}