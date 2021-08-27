using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages.Helpers
{
    public static class DiveProfileHelper
    {
        public static DiveProfile InitaliseDiveProfile()
        {
            var diveModel = new Zhl16Buhlmann();
            var diveProfile = new DiveProfile();

            for (int i = 0; i < diveModel.CompartmentCount; i++)
            {
                diveProfile.MaxSurfacePressures.Add(0.0);
                diveProfile.ToleratedAmbientPressures.Add(0.0);
                diveProfile.CompartmentLoad.Add(0.0);

                diveProfile.TissuePressuresNitrogen.Add(0.79);
                diveProfile.TissuePressuresHelium.Add(0.0);
                diveProfile.TissuePressuresTotal.Add(0.0);

                diveProfile.AValues.Add(0.0);
                diveProfile.BValues.Add(0.0);
            }

            return diveProfile;
        }
    }
}