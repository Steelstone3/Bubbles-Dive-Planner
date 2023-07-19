using BubblesDivePlanner.ViewModels.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Planner.Plan.Stage;

namespace BubblesDivePlannerTests.TestFixtures
{
    public static class PlannerTestFixture
    {
        private const int COMPARTMENT_SIZE = 16;

        public static Zhl16bBuhlmann GetDiveModel => new();

        public static DiveStep GetDiveStep => new()
        {
            Depth = 50,
            Time = 10
        };

        public static Cylinder GetCylinder => new()
        {
            Name = "Air",
            Pressure = 200,
            Volume = 12,
            GasMixture = new GasMixture()
            {
                Oxygen = 21,
                Helium = 0
            },
            GasUsage = new GasUsage()
            {
                GasRemaining = 2400,
                GasUsed = 720,
                SurfaceAirConsumptionRate = 12
            }
        };

        public static DiveProfile GetDiveProfile => new(COMPARTMENT_SIZE);

        public static DiveProfile GetDiveProfileResult => new(16)
        {
            OxygenAtPressure = 1.26f,
            NitrogenAtPressure = 4.74000025f,
            HeliumAtPressure = 0f,
            AValues = new float[COMPARTMENT_SIZE] { 1.2559f, 1.0000f, 0.8618f, 0.7562f, 0.6667f, 0.5600f, 0.4947f, 0.4500f, 0.4187f, 0.3798f, 0.3497f, 0.3223f, 0.2850f, 0.2737f, 0.2523f, 0.2327f },
            BValues = new float[COMPARTMENT_SIZE] { 0.5050f, 0.6514f, 0.7222f, 0.7825f, 0.8126f, 0.8434f, 0.8693f, 0.8910f, 0.9092f, 0.9222f, 0.9319f, 0.9403f, 0.9477f, 0.9544f, 0.9602f, 0.9653f },
            MaxSurfacePressures = new float[COMPARTMENT_SIZE] { 3.2361f, 2.5352f, 2.2465f, 2.0342f, 1.8973f, 1.7457f, 1.6451f, 1.5723f, 1.5186f, 1.4642f, 1.4228f, 1.3858f, 1.3402f, 1.3215f, 1.2937f, 1.2686f },
            NitrogenTissuePressures = new float[COMPARTMENT_SIZE] { 4.0417f, 3.0792f, 2.4713f, 2.0243f, 1.6843f, 1.4439f, 1.2634f, 1.13f, 1.0334f, 0.9731f, 0.9337f, 0.9029f, 0.8788f, 0.8596f, 0.8446f, 0.8329f },
            HeliumTissuePressures = new float[COMPARTMENT_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            TotalTissuePressures = new float[COMPARTMENT_SIZE] { 4.0417f, 3.0792f, 2.4713f, 2.0243f, 1.6843f, 1.4439f, 1.2634f, 1.13f, 1.0334f, 0.9731f, 0.9337f, 0.9029f, 0.8788f, 0.8596f, 0.8446f, 0.8329f },
            ToleratedAmbientPressures = new float[COMPARTMENT_SIZE] { 1.4068f, 1.3544f, 1.1624f, 0.9923f, 0.8269f, 0.7455f, 0.6682f, 0.6059f, 0.5589f, 0.5471f, 0.5442f, 0.5459f, 0.5627f, 0.5592f, 0.5687f, 0.5794f },
            CompartmentLoads = new float[COMPARTMENT_SIZE] { 124.89f, 121.46f, 110.01f, 99.51f, 88.77f, 82.71f, 76.8f, 71.87f, 68.05f, 66.46f, 65.62f, 65.15f, 65.57f, 65.05f, 65.29f, 65.66f }
        };
    }
}
