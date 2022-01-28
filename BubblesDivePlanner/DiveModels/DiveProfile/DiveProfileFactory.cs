namespace BubblesDivePlanner.DiveModels.DiveProfile
{
    public class DiveProfileFactory : IDiveProfileFactory
    {
        public IDiveProfileModel CreateDiveProfile(int compartmentSize)
        {
            IDiveProfileModel diveProfileModel = new DiveProfileModel();

            for (int i = 0; i < compartmentSize; i++)
            {
                diveProfileModel.MaxSurfacePressures.Add(0);
                diveProfileModel.ToleratedAmbientPressures.Add(0);
                diveProfileModel.CompartmentLoad.Add(0);
                diveProfileModel.TissuePressuresNitrogen.Add(0.79);
                diveProfileModel.TissuePressuresHelium.Add(0);
                diveProfileModel.TissuePressuresTotal.Add(0);
                diveProfileModel.AValues.Add(0);
                diveProfileModel.BValues.Add(0);
            }

            return diveProfileModel;
        }
    }
}