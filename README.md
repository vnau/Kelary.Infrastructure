# Kelary Infrastructure
[![Nuget Version](https://img.shields.io/nuget/v/Kelary.Infrastructure.svg)](https://www.nuget.org/packages/Kelary.Infrastructure)

Kelary Infrastructure provides essential infrastructure components and helpers for developing WPF applications. This library includes various utilities designed to simplify common tasks in WPF development.

## Features

- **Converters**: A collection of converters for common data transformations.
- **Markup Style Extensions**: Enhancements to XAML markup for cleaner and more maintainable code.
- **Collections**:
  - `ObservableDictionary`: A dictionary that notifies listeners of dynamic changes.
  - `DeepObservableCollection`: An observable collection that tracks changes within nested collections.
- **Services**:
  - **File Dialog Service**: Simplifies file selection dialogs.
  - **Window Navigation Service**: Manages window navigation within an application.
  - **Page Navigation Service**: Handles navigation between pages.

## Installation

You can install Kelary Infrastructure via NuGet:

```sh
dotnet add package Kelary.Infrastructure
```

Or through the NuGet Package Manager in Visual Studio.

## Usage

### Converters

Kelary Infrastructure includes various converters for common tasks. Here’s an example of how to use a converter in XAML:

```xaml
<Window xmlns:infra="clr-namespace:Kelary.Infrastructure.Converters;assembly=Kelary.Infrastructure">
    <Window.Resources>
        <infra:BoolToVisibilityConverter x:Key="BoolToVisibilityConverter"/>
    </Window.Resources>
    <Grid>
        <TextBlock Visibility="{Binding IsVisible, Converter={StaticResource BoolToVisibilityConverter}}" Text="Hello, World!"/>
    </Grid>
</Window>
```

### ObservableDictionary

ObservableDictionary can be used in place of a regular dictionary when you need to notify listeners of changes:
```csharp
using Kelary.Infrastructure.Collections;

var dictionary = new ObservableDictionary<string, string>();
dictionary.Add("key", "value");
dictionary.CollectionChanged += (s, e) => 
{
    // Handle changes
};
```

### DeepObservableCollection

DeepObservableCollection tracks changes within nested collections:
```csharp
using Kelary.Infrastructure.Collections;
using System.Collections.ObjectModel;

var nestedCollection = new DeepObservableCollection<ObservableCollection<string>>
{
    new ObservableCollection<string> { "Item1", "Item2" },
    new ObservableCollection<string> { "Item3", "Item4" }
};
nestedCollection.CollectionChanged += (s, e) =>
{
    // Handle changes
};
```

### File Dialog Service

Simplify file dialogs with the File Dialog Service:

```csharp
using Kelary.Infrastructure.Services;

var fileDialogService = new FileDialogService();
string filePath = fileDialogService.OpenFileDialog("Select a file", "Text Files|*.txt");
```

### Window Navigation Service

Manage window navigation within your application:
```csharp
using Kelary.Infrastructure.Services;
using System.Windows;

public partial class MainWindow : Window
{
    private readonly WindowNavigationService _navigationService;

    public MainWindow()
    {
        InitializeComponent();
        _navigationService = new WindowNavigationService(this);
    }

    private void OpenNewWindow()
    {
        var newWindow = new AnotherWindow();
        _navigationService.Navigate(newWindow);
    }
}
```

### Page Navigation Service

Handle page navigation within a Frame control:
```csharp
using Kelary.Infrastructure.Services;
using System.Windows.Controls;

public partial class MainPage : Page
{
    private readonly PageNavigationService _navigationService;

    public MainPage()
    {
        InitializeComponent();
        _navigationService = new PageNavigationService(this.NavigationService);
    }

    private void NavigateToAnotherPage()
    {
        var anotherPage = new AnotherPage();
        _navigationService.Navigate(anotherPage);
    }
}
```

## License

This project is licensed under the MIT License. See the LICENSE file for details.
