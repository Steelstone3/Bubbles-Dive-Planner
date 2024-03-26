using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ReactiveUI;

namespace BubblesDivePlanner.Views
{
    public class ViewLocator : IDataTemplate
    {
        public Control Build(object data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            string name = data.GetType().FullName!.Replace("ViewModel", "View");
            Type type = Type.GetType(name);

            return type is null ? new TextBlock { Text = "Not Found: " + name } : (Control)Activator.CreateInstance(type)!;
        }

        public bool Match(object data)
        {
            return data is null ? throw new ArgumentNullException(nameof(data)) : data is ReactiveObject;
        }
    }
}