namespace BubblesDivePlanner.Contracts.ViewModels.Results
{
    public interface IDiveParametersResultViewModel
    {
        string DiveProfileStepHeader { get; set; }

        string DiveModelUsed { get; set; }

        int Depth { get; set; }
        
        int Time { get; set; }

        string GasName { get; set; }

        double Oxygen { get; set; }

        double Helium { get; set; }

        double Nitrogen { get; set; }

        double DiveCeiling { get; set; }
    }
}