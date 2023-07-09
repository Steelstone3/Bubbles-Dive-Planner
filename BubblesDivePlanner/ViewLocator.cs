using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using BubblesDivePlanner.ViewModels;

namespace BubblesDivePlanner;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        string name = data.GetType().FullName!.Replace("ViewModel", "View");
        Type? type = Type.GetType(name);

        if (type == null)
        {
            return new TextBlock { Text = "Not Found: " + name };
        }

        return (Control)Activator.CreateInstance(type)!;
    }

    public bool Match(object? data)
    {
        return data == null ? throw new ArgumentNullException(nameof(data)) : data is ViewModelBase;
    }
}