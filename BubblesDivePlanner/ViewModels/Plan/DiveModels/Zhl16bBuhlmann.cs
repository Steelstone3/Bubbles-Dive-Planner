using BubblesDivePlanner.ViewModels.Plan.DiveModels;

namespace BubblesDivePlanner.ViewModels.Model.Plan.DiveModels
{
    public class Zhl16bBuhlmann : IDiveModel
    {
        public Zhl16bBuhlmann()
        {
            const byte compartments = 16;
            Name = "Zhl16-B Model";
            Compartments = compartments;
            NitrogenHalfTimes = new float[compartments] { 4.0f, 8.0f, 12.5f, 18.5f, 27.0f, 38.3f, 54.3f, 77.0f, 109.0f, 146.0f, 187.0f, 239.0f, 305.0f, 390.0f, 498.0f, 635.0f };
            HeliumHalfTimes = new float[compartments] { 1.51f, 3.02f, 4.72f, 6.99f, 10.21f, 14.48f, 20.53f, 29.11f, 41.20f, 55.19f, 70.69f, 90.34f, 115.29f, 147.42f, 188.24f, 240.03f };
            AValuesNitrogen = new float[compartments] { 1.2559f, 1.0000f, 0.8618f, 0.7562f, 0.6667f, 0.5600f, 0.4947f, 0.4500f, 0.4187f, 0.3798f, 0.3497f, 0.3223f, 0.2850f, 0.2737f, 0.2523f, 0.2327f };
            BValuesNitrogen = new float[compartments] { 0.5050f, 0.6514f, 0.7222f, 0.7825f, 0.8126f, 0.8434f, 0.8693f, 0.8910f, 0.9092f, 0.9222f, 0.9319f, 0.9403f, 0.9477f, 0.9544f, 0.9602f, 0.9653f };
            AValuesHelium = new float[compartments] { 1.7424f, 1.3830f, 1.1919f, 1.0458f, 0.9220f, 0.8205f, 0.7305f, 0.6502f, 0.5950f, 0.5545f, 0.5333f, 0.5189f, 0.5181f, 0.5176f, 0.5172f, 0.5119f };
            BValuesHelium = new float[compartments] { 0.4245f, 0.5747f, 0.6527f, 0.7223f, 0.7582f, 0.7957f, 0.8279f, 0.8553f, 0.8757f, 0.8903f, 0.8997f, 0.9073f, 0.9122f, 0.9171f, 0.9217f, 0.9267f };
            DiveProfile = new DiveProfile(compartments);
        }

        public string Name { get; }
        public byte Compartments { get; }
        public float[] NitrogenHalfTimes { get; }
        public float[] HeliumHalfTimes { get; }
        public float[] AValuesNitrogen { get; }
        public float[] BValuesNitrogen { get; }
        public float[] AValuesHelium { get; }
        public float[] BValuesHelium { get; }
        public IDiveProfile DiveProfile { get; }
    }
}