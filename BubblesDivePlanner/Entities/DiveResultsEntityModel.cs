using System.Collections.Generic;
using BubblesDivePlanner.Contracts.Entities;
using BubblesDivePlanner.Models.Results;

namespace BubblesDivePlanner.Entities
{
    public class DiveResultsEntityModel : IEntityModel
    {
        public string DiveProfileStepHeader { get; set; }
        public string DiveModelUsed { get; set; }
        public int Depth { get; set; }
        public int Time { get; set; }
        public string GasName { get; set; }
        public double Oxygen { get; set; }
        public double Helium { get; set; }
        public double Nitrogen { get; set; }
        public List<DiveResultsModel> DiveResults { get; set; } = new List<DiveResultsModel>();
    }
}