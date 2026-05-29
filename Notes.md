# Notes

```cs

// Styles/GlobalStyles.cs
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Markup.Declarative; // Optional, but great for fluent brushes

namespace AvaloniaMarkupExample.Styles
{
    public static class GlobalStyles
    {
        public static void Load(Styles appStyles)
        {
            // 1. Create a global style for all Buttons
            var buttonStyle = new Style(s => s.OfType<Button>());
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, Brush.Parse("#007ACC")));
            buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
            buttonStyle.Setters.Add(new Setter(Button.CornerRadiusProperty, new Avalonia.CornerRadius(6)));
            buttonStyle.Setters.Add(new Setter(Button.PaddingProperty, new Avalonia.Thickness(12, 6)));

            // 2. Create a global style for all TextBlocks
            var textBlockStyle = new Style(s => s.OfType<TextBlock>());
            textBlockStyle.Setters.Add(new Setter(TextBlock.ForegroundProperty, Brush.Parse("#E0E0E0")));
            textBlockStyle.Setters.Add(new Setter(TextBlock.FontFamilyProperty, "Segoe UI, Arial"));

            // 3. Create a style targeting a specific class (like a CSS class)
            // This will only apply to TextBlocks that have the "Header" class added to them
            var headerStyle = new Style(s => s.OfType<TextBlock>().Class("Header"));
            headerStyle.Setters.Add(new Setter(TextBlock.FontSizeProperty, 24.0));
            headerStyle.Setters.Add(new Setter(TextBlock.FontWeightProperty, FontWeight.Bold));
            headerStyle.Setters.Add(new Setter(TextBlock.MarginProperty, new Avalonia.Thickness(0, 0, 0, 10)));

            // Add them all to the application's style collection
            appStyles.Add(buttonStyle);
            appStyles.Add(textBlockStyle);
            appStyles.Add(headerStyle);
        }
    }
}
```

Avalonia MVVM Todo App Example
https://gemini.google.com/share/6895b0e16ed9
Created with 3.5 Flash May 29, 2026 at 02:47 AM Published May 29, 2026 at 02:57 AM

I want to create a model controller view style application using C# and Avaolia show an example

Creating a Model-View-ViewModel (MVVM) or Model-View-Controller (MVC) application in Avalonia UI is a fantastic choice.

While classic MVC is traditional for web apps, Avalonia UI (like WPF and MAUI) natively uses MVVM. In MVVM, the ViewModel acts exactly like a Controller—it handles the presentation logic and mediates between the View (UI) and the Model (Data).

Here is a complete, lightweight example of a Todo application using the standard Avalonia MVVM pattern.
1. The Model

The Model represents your data structures and business logic. It doesn't care about UI or how data is displayed.
C#

// Models/TodoItem.cs
namespace AvaloniaMvcExample.Models
{
    public class TodoItem
    {
        public string Title { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
    }
}

2. The ViewModel (The "Controller")

The ViewModel acts as the controller. It holds the state of the View and contains the commands/methods that execute when a user interacts with the UI.

To make properties automatically update the UI when they change, we use ObservableObject (from the standard CommunityToolkit.Mvvm package).
C#

// ViewModels/MainWindowViewModel.cs
using System.Collections.ObjectModel;
using System.Windows.Input;
using AvaloniaMvcExample.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaMvcExample.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        // These properties automatically notify the View when changed
        [ObservableProperty]
        private string _newTodoTitle = string.Empty;

        // A collection that automatically updates the UI list when items are added/removed
        public ObservableCollection<TodoItem> TodoItems { get; } = new();

        // The "Action" or command triggered by the View
        [RelayCommand]
        public void AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(NewTodoTitle))
            {
                TodoItems.Add(new TodoItem { Title = NewTodoTitle });
                NewTodoTitle = string.Empty; // Clear the input field
            }
        }
    }
}

3. The View

The View is your XAML file. It defines the layout and binds visual elements directly to the properties and commands in your ViewModel.
XML

<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2000/xaml"
        xmlns:vm="using:AvaloniaMvcExample.ViewModels"
        x:Class="AvaloniaMvcExample.Views.MainWindow"
        x:DataType="vm:MainWindowViewModel"
        Title="Avalonia MVVM/MVC Example" Width="400" Height="450">

    <StackPanel Margin="20" Spacing="10">
        <TextBlock Text="My Todo List" FontSize="20" FontWeight="Bold" HorizontalAlignment="Center"/>

        <DockPanelHorizontalAlignment="Stretch">
            <TextBox Text="{Binding NewTodoTitle, Mode=TwoWay}" 
                     Watermark="Add a new task..." 
                     Width="260"/>
            
            <Button Content="Add" 
                    Command="{Binding AddTodoCommand}" 
                    Width="80" 
                    HorizontalAlignment="Right"/>
        </DockPanel>

        <Separator Margin="0,10"/>

        <ListBox ItemsSource="{Binding TodoItems}" Height="300">
            <ListBox.ItemTemplate>
                <DataTemplate>
                    <CheckBox IsChecked="{Binding IsChecked}" Margin="5">
                        <TextBlock Text="{Binding Title}"/>
                    </CheckBox>
                </DataTemplate>
            </ListBox.ItemTemplate>
        </ListBox>
    </StackPanel>
</Window>

4. Wiring It Together

In your App setup file, you instantly instantiate the View and assign your ViewModel as its context.
C#

// App.axaml.cs
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaMvcExample.ViewModels;
using AvaloniaMvcExample.Views;

namespace AvaloniaMvcExample
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(), // Wiring the "Controller" to the View
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}

How to get this running quickly:

    Open your terminal and install the Avalonia templates if you haven't already:
    dotnet new install Avalonia.Templates

    Create a new MVVM project:
    dotnet new avalonia.mvvm -o AvaloniaMvcExample

    This template will automatically install the CommunityToolkit.Mvvm NuGet package used in the code above and set up the folders for you.

Is there no direct way of using it

No, there is no native or direct way to force a modern desktop framework like Avalonia UI to use a traditional web-style MVC pattern (where a separate Controller intercepts user input, processes it, and manually pushes a new View to the screen).

Avalonia is built from the ground up for Data Binding.

If you try to bypass MVVM and force a strict MVC structure, you end up fighting the framework. Here is why it doesn't work directly, alongside how you can bridge the gap if you really want an MVC mindset.
Why Desktop Frameworks Don't Do Direct MVC

In a web MVC architecture, the workflow is linear: the user clicks a link, a Controller receives the HTTP request, grabs data from the Model, and injects it into a View to render a brand-new page.

In a desktop app like Avalonia, the View is alive and constantly running in memory.

    The Sync Problem: If a user types into a textbox, the UI needs to update its internal state instantly. Without Data Binding (MVVM), your Controller would need to manually listen to every single keypress event, grab the text, update the Model, and then manually tell the UI to redraw. This results in massive amounts of boilerplate "glue code."

    The Solution: Avalonia uses XAML data binding to link the UI directly to a ViewModel. When the user types, the variable updates automatically.

The "Code-Behind" Escape Hatch (Closest to Direct MVC)

If you absolutely want a direct controller-style experience where you write explicit C# code to handle user actions instead of using XAML bindings, you can use the Code-Behind file (.axaml.cs).

In this approach, your View is the XAML, your Model is your data, and the Code-Behind acts directly as your Controller by handling events.
Example: Direct Event-Driven Approach

The View (MainWindow.axaml)
Instead of binding to commands, you point UI events directly to C# methods:
XML

<StackPanel Margin="20">
    <TextBox x:Name="TodoInput" Watermark="Type here..."/>
    <Button Content="Add Todo" Click="OnAddTodoClicked"/> 
    <ListBox x:Name="TodoGrid"/>
</StackPanel>

The "Controller" (MainWindow.axaml.cs)
You use the code-behind to manipulate the UI elements directly by their name:
C#

using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;

namespace AvaloniaExample
{
    public partial class MainWindow : Window
    {
        // The Model (Local State)
        private List<string> _todoList = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        // The "Controller" Action
        public void OnAddTodoClicked(object sender, RoutedEventArgs e)
        {
            string text = TodoInput.Text;
            
            if (!string.IsNullOrWhiteSpace(text))
            {
                // 1. Update Model
                _todoList.Add(text); 
                
                // 2. Directly manipulate the View
                TodoGrid.ItemsSource = null; // Reset binding
                TodoGrid.ItemsSource = _todoList; // Refresh list
                TodoInput.Text = string.Empty; // Clear input
            }
        }
    }
}

Why this isn't recommended for large apps:

While this feels more like a direct "Controller" layout, as your app grows, this file becomes thousands of lines long. You have to manually manage UI states (e.g., enabling/disabling buttons, hiding loaders, clearing text fields), which is incredibly prone to bugs.

Embracing MVVM and letting the data binding handle the UI synchronization will save you massive amounts of development time in Avalonia.

Is there a more direct to the point front end

If you are looking for a more direct, straight-to-the-point C# front-end where you don't have to deal with XAML, complex data binding architectures, or the separation of MVVM, you have two excellent modern alternatives.
1. Avalonia C# Markup (The "Flutter/SwiftUI" Way)

If you want to stick with Avalonia but hate XAML and want to build your UI directly in pure C# code, you can use Avalonia.Markup.Declarative.

It removes the strict separation of files. You write your UI, logic, and state in a single, highly readable C# file, much like Flutter, SwiftUI, or React.
Example:
C#

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Declarative;
using System.Collections.ObjectModel;

public class MainWindow : Window
{
    // Simple state right inside the class
    private string _newTodo = "";
    private ObservableCollection<string> _todos = new();

    public MainWindow()
    {
        Title = "Direct C# UI";
        Width = 400; Height = 500;

        // Define UI directly using a fluent C# API
        Content = new StackPanel()
            .Margin(20)
            .Spacing(10)
            .Children(
                new TextBlock().Text("Todo List").FontSize(20).HorizontalAlignment_Center(),
                
                new DockPanel().Children(
                    new TextBox()
                        .Watermark("What needs doing?")
                        .Width(250)
                        .Dock(Dock.Left)
                        .OnTextChanged(e => _newTodo = e), // Direct state change
                    
                    new Button()
                        .Content("Add")
                        .Dock(Dock.Right)
                        .OnClick(e => {
                            if (!string.IsNullOrEmpty(_newTodo)) {
                                _todos.Add(_newTodo);
                            }
                        })
                ),

                new ListBox().ItemsSource(_todos)
            );
    }
}

    Why it's direct: Everything is in one language (C#), in one file. You can see your logic right next to your UI layout.

2. ImGui.NET (The "Ultimate Direct" Way)

If you want something completely stripped of architecture, look at ImGui.NET (Immediate Mode GUI).

Immediate mode means there are no "Objects" or "Views" held in memory. The UI is completely destroyed and redrawn from scratch 60 times a second based on your variables. If a variable changes, the UI updates instantly. It doesn't get more direct than this.
Example:
C#

// This runs inside your application's render/update loop (60 frames per second)
private string _todoInput = "";
private List<string> _todos = new();

public void RenderUI()
{
    ImGui.Begin("My Todo List");

    // 1. Text input directly modifies the C# string variable
    ImGui.InputText("Task Name", ref _todoInput, 100);

    // 2. Button click directly executes standard C# logic
    if (ImGui.Button("Add Todo"))
    {
        if (!string.IsNullOrWhiteSpace(_todoInput))
        {
            _todos.Add(_todoInput);
            _todoInput = ""; // Reset
        }
    }

    ImGui.Separator();

    // 3. Loop and render text directly from the list
    foreach (var todo in _todos)
    {
        ImGui.Text(todo);
    }

    ImGui.End();
}

    Why it's direct: There is no "binding" or "view model." You literally just say ImGui.Text(_todoInput) and it appears on screen. If the list updates, the text updates.

    Best used for: Internal tools, game development dashboards, and utility applications where speed of development is more important than beautiful designer aesthetics.

Summary: Which should you choose?

    If you like Avalonia's cross-platform power but want a cleaner, single-file approach without XAML, go with Avalonia C# Markup.

    If you want to completely bypass UI architectures and write raw, procedural layout logic, look into ImGui.NET.

I like the markup idea how do I add user controls

Using User Controls (reusable UI components) in Avalonia C# Markup is straightforward. Since everything is pure C#, a User Control is just a standard C# class that inherits from UserControl. You design its layout inside the constructor using the same fluent API.

Here is how you create a custom User Control and use it inside your main window.
1. Create the User Control

Let's create a reusable TodoItemControl that displays a checkbox, a task title, and a delete button.
C#

// Controls/TodoItemControl.cs
using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Declarative;

namespace AvaloniaMarkupExample.Controls
{
    public class TodoItemControl : UserControl
    {
        // Expose an event so the parent window knows when the delete button is clicked
        public event Action? OnDeleteRequested;

        public TodoItemControl(string taskTitle)
        {
            // Define the UI for this specific control
            Content = new Border()
                .Background("#2a2a2a")
                .CornerRadius(4)
                .Padding(10)
                .Margin(0, 0, 0, 5)
                .Child(
                    new DockPanel().Children(
                        // 1. Checkbox on the left
                        new CheckBox()
                            .Dock(Dock.Left)
                            .VerticalAlignment(VerticalAlignment.Center),

                        // 2. Delete Button on the right
                        new Button()
                            .Content("✕")
                            .Foreground("Red")
                            .Dock(Dock.Right)
                            .VerticalAlignment(VerticalAlignment.Center)
                            .OnClick(e => OnDeleteRequested?.Invoke()), // Trigger the event

                        // 3. Title fills the remaining middle space
                        new TextBlock()
                            .Text(taskTitle)
                            .VerticalAlignment(VerticalAlignment.Center)
                            .Margin(10, 0, 0, 0)
                    )
                );
        }
    }
}

2. Use it in your Main Window

To add your user control to a layout (like a StackPanel), you instantiate it with new TodoItemControl("Task Name") just like any other built-in Avalonia control.

Because we are dealing with a dynamic list of controls, we can use a container like a StackPanel and name it (x:Name equivalent) so we can easily add or remove your custom controls from it in code.
C#

// Windows/MainWindow.cs
using Avalonia.Controls;
using Avalonia.Markup.Declarative;
using AvaloniaMarkupExample.Controls;

namespace AvaloniaMarkupExample.Windows
{
    public class MainWindow : Window
    {
        private string _newTodoText = string.Empty;
        
        // We keep a reference to the StackPanel container to dynamically add/remove controls
        private readonly StackPanel _todoContainer; 

        public MainWindow()
        {
            Title = "C# Markup with User Controls";
            Width = 400; Height = 500;

            Content = new StackPanel()
                .Margin(20)
                .Spacing(15)
                .Children(
                    new TextBlock().Text("My Tasks").FontSize(22).FontWeight_Bold(),

                    // Input Row
                    new DockPanel().Children(
                        new TextBox()
                            .Watermark("Add a new task...")
                            .Width(260).Dock(Dock.Left)
                            .OnTextChanged(text => _newTodoText = text),

                        new Button()
                            .Content("Add")
                            .Width(80).Dock(Dock.Right)
                            .OnClick(e => AddNewTask())
                    ),

                    new Separator(),

                    // Scrollable area holding our custom User Controls
                    new ScrollViewer().Content(
                        new StackPanel()
                            .Spacing(5)
                            .Assign(out _todoContainer) // Assigns this panel to the variable above
                    )
                );
        }

        private void AddNewTask()
        {
            if (string.IsNullOrWhiteSpace(_newTodoText)) return;

            // 1. Create an instance of your custom User Control
            var todoItem = new TodoItemControl(_newTodoText);

            // 2. Handle its delete event
            todoItem.OnDeleteRequested += () => 
            {
                _todoContainer.Children.Remove(todoItem);
            };

            // 3. Inject it directly into the UI container
            _todoContainer.Children.Add(todoItem);
        }
    }
}

Why this is powerful:

    Encapsulation: The internal layout, margins, padding, and individual delete button logic belong entirely to TodoItemControl. Your MainWindow doesn't need to know how a row is styled.

    Type Safety: No Magic strings or XAML parsing errors. If you misspell a property or pass the wrong data type to your user control, the C# compiler catches it instantly before the app even runs.

How would I put add new task in a controller

Since you are using the C# Markup approach, you can easily separate your UI layout from your business logic by moving the "Add New Task" action into a dedicated Controller class.

Because we are working in pure C#, we can pass references or use standard C# events to let the Controller talk to the View.

Here is how you extract that logic cleanly:
1. Create the Controller

The Controller handles the application state and business rules. It shouldn't know about pixels, margins, or fonts, but it does manage the list of tasks.
C#

// Controllers/TodoController.cs
using System;
using System.Collections.Generic;

namespace AvaloniaMarkupExample.Controllers
{
    public class TodoController
    {
        // Internal data state (The Model layer)
        private readonly List<string> _tasks = new();

        // Events that the View can listen to
        public event Action<string>? TaskAdded;
        public event Action<string>? TaskValidationError;

        public void HandleAddTask(string taskTitle)
        {
            // 1. Business Logic / Validation
            if (string.IsNullOrWhiteSpace(taskTitle))
            {
                TaskValidationError?.Invoke("Task title cannot be empty!");
                return;
            }

            if (_tasks.Contains(taskTitle))
            {
                TaskValidationError?.Invoke("This task already exists!");
                return;
            }

            // 2. Update the Data State
            _tasks.Add(taskTitle);

            // 3. Notify the View that a task was successfully added
            TaskAdded?.Invoke(taskTitle);
        }

        public void HandleDeleteTask(string taskTitle)
        {
            _tasks.Remove(taskTitle);
        }
    }
}

2. Update the View to use the Controller

Now, your MainWindow creates an instance of the TodoController, registers listeners for its events, and routes UI clicks straight to the controller methods.
C#

// Windows/MainWindow.cs
using Avalonia.Controls;
using Avalonia.Markup.Declarative;
using AvaloniaMarkupExample.Controllers;
using AvaloniaMarkupExample.Controls;

namespace AvaloniaMarkupExample.Windows
{
    public class MainWindow : Window
    {
        private readonly TodoController _controller;
        private readonly StackPanel _todoContainer; 
        private string _newTodoText = string.Empty;

        public MainWindow()
        {
            // Initialize the Controller
            _controller = new TodoController();

            // Subscribe to Controller events (What happens when data changes)
            _controller.TaskAdded += OnTaskAddedByController;
            _controller.TaskValidationError += OnValidationError;

            Title = "C# Markup with Controller";
            Width = 400; Height = 500;

            Content = new StackPanel()
                .Margin(20)
                .Spacing(15)
                .Children(
                    new TextBlock().Text("My Tasks").FontSize(22).FontWeight_Bold(),

                    // Input Row
                    new DockPanel().Children(
                        new TextBox()
                            .Watermark("Add a new task...")
                            .Width(260).Dock(Dock.Left)
                            .OnTextChanged(text => _newTodoText = text),

                        new Button()
                            .Content("Add")
                            .Width(80).Dock(Dock.Right)
                            // Hand off the UI event directly to the controller
                            .OnClick(e => _controller.HandleAddTask(_newTodoText)) 
                    ),

                    new Separator(),

                    new ScrollViewer().Content(
                        new StackPanel()
                            .Spacing(5)
                            .Assign(out _todoContainer) 
                    )
                );
        }

        // --- View Updates Reacting to the Controller ---

        private void OnTaskAddedByController(string title)
        {
            var todoItem = new TodoItemControl(title);

            todoItem.OnDeleteRequested += () => 
            {
                _controller.HandleDeleteTask(title); // Tell controller it's deleted
                _todoContainer.Children.Remove(todoItem); // Remove from visual tree
            };

            _todoContainer.Children.Add(todoItem);
        }

        private void OnValidationError(string errorMessage)
        {
            // You could pop up a dialog or style a textblock here
            System.Diagnostics.Debug.WriteLine($"[UI Warning] {errorMessage}");
        }
    }
}

Why this structure is clean:

    Separation of Concerns: Your UI code only handles rendering controls. If tomorrow you want to save tasks to a SQL database instead of an in-memory list, you modify only the TodoController. The View file remains untouched.

    Easy Testing: You can now write a unit test for TodoController.HandleAddTask() to ensure it prevents duplicate or empty items without ever needing to boot up a visual UI window.

Show an example test

Testing this setup is incredibly straightforward. Because your TodoController is a plain C# class that doesn't depend on any Avalonia UI elements or threads, you can test its logic directly using standard testing frameworks like xUnit.

Here is an example of how you would write unit tests to verify your controller's behavior, including how to test the C# events it triggers.
1. Set Up Your Test Class

If you are using xUnit, your test project just needs a reference to your main application project. You can instantiate the controller, simulate user input, and assert that the correct events are fired.
C#

// AvaloniaMarkupExample.Tests/TodoControllerTests.cs
using Xunit;
using AvaloniaMarkupExample.Controllers;

namespace AvaloniaMarkupExample.Tests
{
    public class TodoControllerTests
    {
        [Fact]
        public void HandleAddTask_ValidTitle_FiresTaskAddedEvent()
        {
            // Arrange
            var controller = new TodoController();
            string? receivedTitle = null;
            
            // Subscribe to the event to "catch" the controller's response
            controller.TaskAdded += (title) => receivedTitle = title;

            // Act
            controller.HandleAddTask("Buy milk");

            // Assert
            Assert.Equal("Buy milk", receivedTitle);
        }

        [Fact]
        public void HandleAddTask_EmptyTitle_FiresValidationErrorEvent()
        {
            // Arrange
            var controller = new TodoController();
            string? errorMessage = null;
            bool taskWasAdded = false;

            controller.TaskValidationError += (msg) => errorMessage = msg;
            controller.TaskAdded += (_) => taskWasAdded = true;

            // Act
            controller.HandleAddTask("   "); // Testing whitespace validation

            // Assert
            Assert.NotNull(errorMessage);
            Assert.Contains("cannot be empty", errorMessage);
            Assert.False(taskWasAdded); // Ensure the success event was never fired
        }

        [Fact]
        public void HandleAddTask_DuplicateTitle_PreventsDoubleAdding()
        {
            // Arrange
            var controller = new TodoController();
            int addCount = 0;
            string? errorMessage = null;

            controller.TaskAdded += (_) => addCount++;
            controller.TaskValidationError += (msg) => errorMessage = msg;

            // Act
            controller.HandleAddTask("Read a book");
            controller.HandleAddTask("Read a book"); // Duplicate entry

            // Assert
            Assert.Equal(1, addCount); // Should only have successfully added once
            Assert.NotNull(errorMessage);
            Assert.Contains("already exists", errorMessage);
        }
    }
}

How to Run This Test

    Create a test project in your solution terminal:
    Bash

    dotnet new xunit -o AvaloniaMarkupExample.Tests

    Link your original app to the test project:
    Bash

    dotnet add AvaloniaMarkupExample.Tests reference AvaloniaMarkupExample

    Run the tests:
    Bash

    dotnet test

Because your UI layout and application logic are strictly decoupled via the controller, you can effortlessly maintain high test coverage on your app's core features without ever needing to mock UI windows or click events!
