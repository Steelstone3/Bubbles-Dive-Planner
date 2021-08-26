using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using BubblesDivePlanner.Controllers.DataAccess.DataMappers;
using BubblesDivePlanner.Controllers.DataAccess.Serialisers;
using BubblesDivePlanner.ViewModels;

namespace BubblesDivePlanner.Controllers.Header
{
    public class LoadApplicationStateController
    {
        //TODO AH complete this so that it works
        public async void LoadApplication(MainWindowViewModel mainWindowViewModel)
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
                //TODO AH this is the focus area
                var applicationLoader = new ApplicationSerialiser();
                var applicationConverter = new ApplicationEntityModelDataMapper();

                var entityModels = applicationLoader.DeserialiseApplication(result[0]);
                applicationConverter.ConvertEntitiesToModels(entityModels.ToList(), mainWindowViewModel);
            }
        }
    }
}