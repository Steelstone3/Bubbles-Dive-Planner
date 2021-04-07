using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using DivePlannerMk3.DataAccessLayer.DataMappers;
using DivePlannerMk3.DataAccessLayer.Serialisers;
using DivePlannerMk3.ViewModels;

namespace DivePlannerMk3.Controllers
{
    public class SaveApplicationStateController
    {
        public async void SaveApplication(MainWindowViewModel mainWindowViewModel)
        {
             var saveFileDialog = new SaveFileDialog()
            {
                Directory = string.Empty,
                Title = "Save Dive Profile",
                
                InitialFileName = "DivePlan.json",
                
                Filters = new List<FileDialogFilter>()
                {
                    new FileDialogFilter() { Name = "Json", Extensions = { "json" } },
                },
            };

            var result = await saveFileDialog.ShowAsync(new Window());
            if (result != null)
            {
                var applicationConverter = new ApplicationEntityModelDataMapper();
                var applicationSaver = new ApplicationSaveLoad();

                var entityModels = applicationConverter.ConvertModelsToEntities(mainWindowViewModel);
                applicationSaver.SaveApplication(entityModels.ToList());
            }
        }
    }
}