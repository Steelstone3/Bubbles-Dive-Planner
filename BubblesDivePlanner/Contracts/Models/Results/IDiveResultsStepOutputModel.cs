using System.Collections.Generic;

namespace BubblesDivePlanner.Contracts.Models.Results
{
	public interface IDiveResultsStepOutputModel
	{
		List<IDiveProfileResultModel> DiveProfileStepOutput { get; }
	}
}
