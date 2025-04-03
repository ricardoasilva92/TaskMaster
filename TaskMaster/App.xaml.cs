// App.xaml.cs
using System;
using System.Windows;

namespace TaskMaster
{
    public partial class App : Application
    {
    }
}

// Project Setup Instructions:

/*
1. Create a new WPF Application in Visual Studio:
   - Open Visual Studio
   - Click "Create a new project"
   - Select "WPF App (.NET Framework)" or "WPF Application" (.NET Core/.NET 5+)
   - Name it "TaskMaster"

2. Replace the generated MainWindow.xaml with the provided XAML content
   - Delete everything in the default MainWindow.xaml
   - Paste the provided XAML content

3. Replace the MainWindow.xaml.cs with the provided C# code
   - Delete everything in the default MainWindow.xaml.cs
   - Paste the provided C# code

4. Create App.xaml.cs and replace its content if it doesn't match the provided code
   - Make sure the namespace matches your project

5. Build and run the application
   - Press F5 or click the "Start" button

6. Troubleshooting:
   - If you get namespace errors, ensure all files have the same namespace ("TaskMaster")
   - If you get XML parsing errors in XAML, add the missing xmlns:sys="clr-namespace:System;assembly=mscorlib" 
     to the Window element in MainWindow.xaml
   - For additional XAML dependencies, make sure to add the necessary references to your project
*/