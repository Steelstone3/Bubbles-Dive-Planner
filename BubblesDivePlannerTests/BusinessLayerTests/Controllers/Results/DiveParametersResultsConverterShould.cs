using BubblesDivePlanner.Controllers.Converters;
using BubblesDivePlanner.Models.Results;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Controllers.Results
{
    public class DiveParametersResultsConverterShould
    {        
        private DiveParametersResultModel _diveParametersResultModel = new DiveParametersResultModel();
        private DiveParametersResultModelConverter _diveParametersResultsConverter = new DiveParametersResultModelConverter();

        [Fact]
        public void ConvertParametersResultsModelToViewModelOutputStage()
        {
            var diveParameterResultModel = CreateDiveParametersModel();

            var diveParameterResultViewModel = _diveParametersResultsConverter.ConvertToViewModel(diveParameterResultModel);

            Assert.Equal(50, diveParameterResultViewModel.Depth);
            Assert.Equal(10, diveParameterResultViewModel.Time);
            Assert.Equal("Air", diveParameterResultViewModel.GasName);
            Assert.Equal(5, diveParameterResultViewModel.DiveCeiling);
            Assert.Equal("ZHL16", diveParameterResultViewModel.DiveModelUsed);
            Assert.Equal("Dive Step Header", diveParameterResultViewModel.DiveProfileStepHeader);
            Assert.Equal(1, diveParameterResultViewModel.Helium);
            Assert.Equal(21, diveParameterResultViewModel.Oxygen);
            Assert.Equal(78, diveParameterResultViewModel.Nitrogen);
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