using BubblesDivePlanner.Controllers.Converters;
using BubblesDivePlanner.Models.Results;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Models.Results
{
    public class DiveParametersResultsModelShould
    {
        private DiveParametersResultModel _diveParametersResultModel = new DiveParametersResultModel();

        [Fact]
        public void AllowParametersResultsModelToBeSet()
        {
            var diveParameterResultModel = CreateDiveParametersModel();

            Assert.Equal(50, diveParameterResultModel.Depth);
            Assert.Equal(10, diveParameterResultModel.Time);
            Assert.Equal("Air", diveParameterResultModel.GasName);
            Assert.Equal(5, diveParameterResultModel.DiveCeiling);
            Assert.Equal("ZHL16", diveParameterResultModel.DiveModelUsed);
            Assert.Equal("Dive Step Header", diveParameterResultModel.DiveProfileStepHeader);
            Assert.Equal(1, diveParameterResultModel.Helium);
            Assert.Equal(21, diveParameterResultModel.Oxygen);
            Assert.Equal(78, diveParameterResultModel.Nitrogen);
        }
        
        private DiveParametersResultModel CreateDiveParametersModel()
        {
            _diveParametersResultModel.Depth = 50;
            _diveParametersResultModel.Time = 10;
            _diveParametersResultModel.GasName = "Air";
            _diveParametersResultModel.DiveCeiling = 5;
            _diveParametersResultModel.DiveModelUsed = "ZHL16";
            _diveParametersResultModel.DiveProfileStepHeader = "Dive Step Header";
            _diveParametersResultModel.Helium = 1;
            _diveParametersResultModel.Oxygen = 21;
            _diveParametersResultModel.Nitrogen = 78;

            return _diveParametersResultModel;
        }
    }
}