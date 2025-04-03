# TaskMaster

A modern, minimalist todo application for Windows with blue/grey aesthetics, designed to help you manage tasks and goals across different timeframes.

## Features

- 🔄 **Daily Tasks Management**: Keep track of your everyday todos
- 📅 **Goal Tracking**: Separate tabs for weekly, monthly, and yearly goals
- ✅ **Task Completion**: Mark tasks as done with a single click
- 📊 **Progress History**: View all completed tasks with completion dates
- 💾 **Automatic Saving**: All your data is automatically saved between sessions
- 🎨 **Modern UI**: Clean, intuitive interface with blue/grey color scheme

## Getting Started

### Prerequisites

- Windows operating system
- NET 8.0+

### Installation

#### Option 1: Install from Release

1. Download the latest release from the [Releases](https://github.com/yourusername/TaskMaster/releases) page
2. Extract the ZIP file to your preferred location
3. Run `TaskMaster.exe`

#### Option 2: Build from Source

1. Clone the repository:
   ```
   git clone https://github.com/ricardoasilva92/TaskMaster
   ```

2. Open the solution file (`TaskMaster.sln`) in Visual Studio

3. Build the solution (Press F6 or use Build > Build Solution)

4. Run the application (Press F5 or use Debug > Start Debugging)

## Usage

### Adding Tasks

1. Select the appropriate tab (Daily, Weekly, Monthly, or Yearly)
2. Type your task in the text box
3. Click "Add Task" or press Enter

### Completing Tasks

- Click the ✓ button next to a task to mark it as complete
- Completed tasks will appear in the "Completed" tab

### Deleting Tasks

- Click the ✕ button next to a task to delete it permanently

## Architecture

TaskMaster is built using:

- WPF (Windows Presentation Foundation)
- MVVM (Model-View-ViewModel) design pattern
- XML serialization for data persistence

## Project Structure

- `MainWindow.xaml` & `MainWindow.xaml.cs` - Main UI and event handlers
- `ViewModel.cs` - Business logic and data binding
- `Task.cs` - Data model for tasks
- `App.xaml` & `App.xaml.cs` - Application entry point

## Data Storage

TaskMaster saves your data in:
```
%AppData%\TaskMaster\tasks.xml
```

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Roadmap

- [ ] Add task priorities
- [ ] Implement due dates and reminders
- [ ] Add dark mode theme
- [ ] Create mobile companion app
- [ ] Add cloud synchronization

## Acknowledgments

- Icon made by [Freepik](https://www.freepik.com) from [Flaticon](https://www.flaticon.com/)
- UI design inspired by Microsoft's Fluent Design System