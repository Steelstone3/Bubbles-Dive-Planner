namespace BubblesDivePlanner.DiveModels.DiveProfile
{
    public interface IDiveProfileFactory
    {
        IDiveProfileModel CreateDiveProfile(int compartmentSize);
    }
}