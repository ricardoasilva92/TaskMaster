using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Data;

namespace TaskMaster
{
    // Main application window
    public partial class MainWindow : Window
    {
        public ViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new ViewModel();
            DataContext = ViewModel;

            // Set current date display
            DateDisplay.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy");
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewTaskText.Text))
            {
                ViewModel.AddTask(NewTaskText.Text, "Daily");
                NewTaskText.Text = string.Empty;
            }
        }

        private void AddWeeklyGoal_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewWeeklyGoalText.Text))
            {
                ViewModel.AddTask(NewWeeklyGoalText.Text, "Weekly");
                NewWeeklyGoalText.Text = string.Empty;
            }
        }

        private void AddMonthlyGoal_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewMonthlyGoalText.Text))
            {
                ViewModel.AddTask(NewMonthlyGoalText.Text, "Monthly");
                NewMonthlyGoalText.Text = string.Empty;
            }
        }

        private void AddYearlyGoal_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewYearlyGoalText.Text))
            {
                ViewModel.AddTask(NewYearlyGoalText.Text, "Yearly");
                NewYearlyGoalText.Text = string.Empty;
            }
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Task task)
            {
                ViewModel.CompleteTask(task);
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Task task)
            {
                ViewModel.DeleteTask(task);
            }
        }
    }

    // ViewModel for data binding
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Task> _allTasks;
        private string _selectedCategory = "Daily";

        public ObservableCollection<Task> AllTasks
        {
            get { return _allTasks; }
            set
            {
                _allTasks = value;
                OnPropertyChanged(nameof(AllTasks));
                OnPropertyChanged(nameof(DailyTasks));
                OnPropertyChanged(nameof(WeeklyGoals));
                OnPropertyChanged(nameof(MonthlyGoals));
                OnPropertyChanged(nameof(YearlyGoals));
                OnPropertyChanged(nameof(CompletedTasks));
            }
        }

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public IEnumerable<Task> DailyTasks => AllTasks.Where(t => t.Category == "Daily" && !t.IsCompleted);
        public IEnumerable<Task> WeeklyGoals => AllTasks.Where(t => t.Category == "Weekly" && !t.IsCompleted);
        public IEnumerable<Task> MonthlyGoals => AllTasks.Where(t => t.Category == "Monthly" && !t.IsCompleted);
        public IEnumerable<Task> YearlyGoals => AllTasks.Where(t => t.Category == "Yearly" && !t.IsCompleted);
        public IEnumerable<Task> CompletedTasks => AllTasks.Where(t => t.IsCompleted).OrderByDescending(t => t.CompletedDate);

        public ViewModel()
        {
            LoadTasks();
        }

        public void AddTask(string description, string category)
        {
            AllTasks.Add(new Task
            {
                Id = Guid.NewGuid(),
                Description = description,
                Category = category,
                CreatedDate = DateTime.Now,
                IsCompleted = false
            });
            SaveTasks();
        }

        public void CompleteTask(Task task)
        {
            var taskToUpdate = AllTasks.FirstOrDefault(t => t.Id == task.Id);
            if (taskToUpdate != null)
            {
                taskToUpdate.IsCompleted = true;
                taskToUpdate.CompletedDate = DateTime.Now;
                OnPropertyChanged(nameof(DailyTasks));
                OnPropertyChanged(nameof(WeeklyGoals));
                OnPropertyChanged(nameof(MonthlyGoals));
                OnPropertyChanged(nameof(YearlyGoals));
                OnPropertyChanged(nameof(CompletedTasks));
                SaveTasks();
            }
        }

        public void DeleteTask(Task task)
        {
            AllTasks.Remove(task);
            SaveTasks();
        }

        private void SaveTasks()
        {
            string appDataPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TaskMaster");

            Directory.CreateDirectory(appDataPath);
            string filePath = System.IO.Path.Combine(appDataPath, "tasks.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Task>));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, AllTasks);
            }
        }

        private void LoadTasks()
        {
            string appDataPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TaskMaster");

            string filePath = System.IO.Path.Combine(appDataPath, "tasks.xml");

            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Task>));
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    AllTasks = (ObservableCollection<Task>)serializer.Deserialize(stream);
                }
            }
            else
            {
                AllTasks = new ObservableCollection<Task>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Task model
    public class Task : INotifyPropertyChanged
    {
        private Guid _id;
        private string _description;
        private string _category;
        private DateTime _createdDate;
        private bool _isCompleted;
        private DateTime? _completedDate;

        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set
            {
                _createdDate = value;
                OnPropertyChanged(nameof(CreatedDate));
            }
        }

        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        public DateTime? CompletedDate
        {
            get { return _completedDate; }
            set
            {
                _completedDate = value;
                OnPropertyChanged(nameof(CompletedDate));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}