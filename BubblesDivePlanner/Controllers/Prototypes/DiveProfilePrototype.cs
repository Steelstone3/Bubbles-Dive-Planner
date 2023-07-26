using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Planner.DiveModels;

namespace BubblesDivePlanner.Controllers.Prototypes
{
    public static class DiveProfilePrototype
    {
        public static IDiveProfile DeepClone(IDiveProfile diveProfile)
        {
            return new DiveProfile(0)
            {
                MaxSurfacePressures = diveProfile.MaxSurfacePressures,
                NitrogenTissuePressures = diveProfile.NitrogenTissuePressures,
                HeliumTissuePressures = diveProfile.HeliumTissuePressures,
                TotalTissuePressures = diveProfile.TotalTissuePressures,
                ToleratedAmbientPressures = diveProfile.ToleratedAmbientPressures,
                AValues = diveProfile.AValues,
                BValues = diveProfile.BValues,
                CompartmentLoads = diveProfile.CompartmentLoads,
                OxygenAtPressure = diveProfile.OxygenAtPressure,
                HeliumAtPressure = diveProfile.HeliumAtPressure,
                NitrogenAtPressure = diveProfile.NitrogenAtPressure,
            };
        }
    }
}