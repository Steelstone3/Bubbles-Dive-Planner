using System;
using System.Collections.Generic;
using DivePlannerMk3.Contracts;
using Newtonsoft.Json;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class DiveInfoViewModel : ViewModelBase, IModelConverter
    {
        public DiveInfoViewModel()
        {
        }

        private InfoDecompressionProfileViewModel _decompressionProfile = new InfoDecompressionProfileViewModel();
        public InfoDecompressionProfileViewModel DecompressionProfile
        {
            get => _decompressionProfile;
            set => this.RaiseAndSetIfChanged(ref _decompressionProfile, value);
        }

        private DiveCeilingViewModel _diveCeilingViewModel = new DiveCeilingViewModel();
        public DiveCeilingViewModel DiveCeilingViewModel
        {
            get => _diveCeilingViewModel;
            set => this.RaiseAndSetIfChanged(ref _diveCeilingViewModel, value);
        }

        //private CnsToxicityViewModel _cnsToxicityViewModel = new CnsToxicityViewModel();
        public CnsToxicityViewModel CnsToxicityViewModel
        {
            get; set;
        } = new CnsToxicityViewModel();

        private InfoDiveModelSelectedReadOnlyViewModel _infoDiveModelSelectedReadOnly = new InfoDiveModelSelectedReadOnlyViewModel();
        public InfoDiveModelSelectedReadOnlyViewModel InfoDiveModelSelectedReadOnly
        {
            get => _infoDiveModelSelectedReadOnly;
            set => this.RaiseAndSetIfChanged(ref _infoDiveModelSelectedReadOnly, value);
        }

        public void CalculateDiveStep(IEnumerable<double> toleratedAmbientPressures)
        {
            UpdateUiVisibility();
            DiveCeilingViewModel.CalculateDiveCeiling(toleratedAmbientPressures);
        }

        private void UpdateUiVisibility()
        {
            InfoDiveModelSelectedReadOnly.IsUiVisible = true;
            DiveCeilingViewModel.IsUiVisible = true;
            CnsToxicityViewModel.IsUiVisible = true;

            //TODO AH complexity to be added later true when user needs to decompress
            DecompressionProfile.IsUiVisible = true;
        }

        public void EntityToModel()
        {
            throw new NotImplementedException();
        }

        public void ModelToEntity()
        {
            throw new NotImplementedException();
        }
    }
}
