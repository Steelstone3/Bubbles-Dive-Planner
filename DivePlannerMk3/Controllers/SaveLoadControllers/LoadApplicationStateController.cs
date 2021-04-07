using System.Collections.Generic;
using Avalonia.Controls;
using DivePlannerMk3.DataAccessLayer.DataMappers;
using DivePlannerMk3.DataAccessLayer.Serialisers;

namespace DivePlannerMk3.Controllers
{
    public class LoadApplicationStateController
    {
        public async void LoadApplication()
        {
            var loadFileDialog = new OpenFileDialog()
            {
                AllowMultiple = false,
                Title = "Load Dive Profile",

                Filters = new List<FileDialogFilter>()
                {
                    new FileDialogFilter() { Name = "Json", Extensions = { "json" } },
                },
            };

            var result = await loadFileDialog.ShowAsync(new Window());
            if (result != null)
            {
                var applicationLoader = new ApplicationSaveLoad();
                var applicationConverter = new ApplicationEntityModelDataMapper();

                var entityModels = applicationLoader.LoadApplication();
                applicationConverter.ConvertEntitiesToModels(entityModels);
            }
        }

    }
}