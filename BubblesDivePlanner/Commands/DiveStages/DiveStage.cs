using BubblesDivePlanner.Contracts.Commands;

namespace BubblesDivePlanner.Commands.DiveStages
{
    public abstract class DiveStage : IDiveStage
    {
        public int Compartment
        {
            get; set;
        }

        public DiveStage()
        {
            Compartment = 0;
        }

        public void CompartmentCountCheck(int compartmentCount)
        {
            if(Compartment >= compartmentCount)
            {
                Compartment = 0;
            }
            else
            {
                Compartment += 1;
            }
        }

        public abstract void RunStage();
    }
}