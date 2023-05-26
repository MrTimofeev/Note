using Note.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Note.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskPage : ContentPage
    {
        public List<TaskGroupModel> Tasks = new List<TaskGroupModel>();

        public string CurrentDate; 

        public TaskPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            LoadDateTask();

            CurrentDate = DateTime.Now.ToShortDateString();
            
            LoadTask();

            // Чтобы подгрузить данные
            base.OnAppearing();
        }

        public async void LoadTask() 
        {
            Tasks.Clear();
            
            Tasks.Add(new TaskGroupModel("Не выполненые:", await App.NoteDB.GetTaskNotCompleteDateAsync(CurrentDate)));
            Tasks.Add(new TaskGroupModel("Выполненые:", await App.NoteDB.GetTaskCompleteDateAsync(CurrentDate)));

            collectionView.ItemsSource = null;

            collectionView.ItemsSource = Tasks;
        }


        #region Комнады
        // Команда прехода на страницу добавления
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TaskAddingPage));
        }
        #endregion

        #region Выбор Задачи
        /// <summary>
        /// Событие выбора задачи (После выбора открывается страница с добавлением задач)
        /// </summary>
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                TaskModel task = (TaskModel)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(
                    $"{nameof(TaskAddingPage)}?{nameof(TaskAddingPage.ItemId)}={task.ID.ToString()}");
            }
        }
        #endregion

        #region Сохранения состояния задачи
        /// <summary>
        /// Событие сохранения состояния задачи
        /// </summary>
        private async void CheckedChanged_Task(object sender, CheckedChangedEventArgs e)
        {
            TaskModel CurrentTask = (TaskModel)((CheckBox)sender).BindingContext;
            var OldTask = await App.NoteDB.GetTaskTextAsync(CurrentTask.Text);

            if (OldTask.Status == true && CurrentTask.Status == false)
            {
                CurrentTask.Status = false; // задача невыполнена 

                await App.NoteDB.SaveTaskAsync(CurrentTask);
                LoadTask();
            }

            if (OldTask.Status == false && CurrentTask.Status == true)
            {
                CurrentTask.Status = true; // задача выполнена 

                await App.NoteDB.SaveTaskAsync(CurrentTask);
                LoadTask();
            }
        }

        #endregion

        /// <summary>
        /// Удаление задачи
        /// </summary>
        private async void DeleteTaskCommand(object sender, EventArgs e)
        {
            TaskModel task = (TaskModel)((SwipeItem)sender).BindingContext;
            await App.NoteDB.DeleteTaskAsync(task);
        }

        /// <summary>
        /// Изменение цвета при нажатии
        /// </summary>
        private void ColorDefault() 
        {
            ElementDay1.BackgroundColor = Color.Black;
            Day1.TextColor = Color.Gray;
            DayOfWeek1.TextColor = Color.Gray;

            ElementDay2.BackgroundColor = Color.Black;
            Day2.TextColor = Color.Gray;
            DayOfWeek2.TextColor = Color.Gray;

            ElementDay3.BackgroundColor = Color.Black;
            Day3.TextColor = Color.Gray;
            DayOfWeek3.TextColor = Color.Gray;

            ElementDay4.BackgroundColor = Color.Black;
            Day4.TextColor = Color.Gray;
            DayOfWeek4.TextColor = Color.Gray;

            ElementDay5.BackgroundColor = Color.Black;
            Day5.TextColor = Color.Gray;
            DayOfWeek5.TextColor = Color.Gray;

            ElementDay6.BackgroundColor = Color.Black;
            Day6.TextColor = Color.Gray;
            DayOfWeek6.TextColor = Color.Gray;

            ElementDay7.BackgroundColor = Color.Black;
            Day7.TextColor = Color.Gray;
            DayOfWeek7.TextColor = Color.Gray;
        }

        private void LoadDateTask()
        {
            DateHelper helper = new DateHelper();
            DayOfWeek1.Text = helper.ShortWeekName(-3);
            Day1.Text = DateTime.Now.AddDays(-3).Day.ToString();

            DayOfWeek2.Text = helper.ShortWeekName(-2);
            Day2.Text = DateTime.Now.AddDays(-2).Day.ToString();

            DayOfWeek3.Text = helper.ShortWeekName(-1);
            Day3.Text = DateTime.Now.AddDays(-1).Day.ToString();

            DayOfWeek4.Text = helper.ShortWeekName(0);
            Day4.Text = DateTime.Now.Day.ToString();

            DayOfWeek5.Text = helper.ShortWeekName(1);
            Day5.Text = DateTime.Now.AddDays(1).Day.ToString();

            DayOfWeek6.Text = helper.ShortWeekName(2);
            Day6.Text = DateTime.Now.AddDays(2).Day.ToString();

            DayOfWeek7.Text = helper.ShortWeekName(3);
            Day7.Text = DateTime.Now.AddDays(3).Day.ToString();
        }


        private async void Day1_Tapped(object sender, EventArgs e)
        {
            ColorDefault();

            ElementDay1.BackgroundColor = Color.Gray;
            Day1.TextColor = Color.Black;
            DayOfWeek1.TextColor = Color.Black;

            CurrentDate = DateTime.Now.AddDays(-3).ToShortDateString().ToString();

            LoadTask();
        }

        private async void Day2_Tapped(object sender, EventArgs e)
        {
            ColorDefault();

            ElementDay2.BackgroundColor = Color.Gray;
            Day2.TextColor = Color.Black;
            DayOfWeek2.TextColor = Color.Black;

            CurrentDate = DateTime.Now.AddDays(-2).ToShortDateString().ToString();

            LoadTask();
        }

        private async void Day3_Tapped(object sender, EventArgs e)
        {
            ColorDefault();

            ElementDay3.BackgroundColor = Color.Gray;
            Day3.TextColor = Color.Black;
            DayOfWeek3.TextColor = Color.Black;

            CurrentDate = DateTime.Now.AddDays(-1).ToShortDateString().ToString();

            LoadTask();
        }

        private async void Day4_Tapped(object sender, EventArgs e)
        {
            ColorDefault();

            ElementDay4.BackgroundColor = Color.Gray;
            Day4.TextColor = Color.Black;
            DayOfWeek4.TextColor = Color.Black;

            CurrentDate = DateTime.Now.AddDays(0).ToShortDateString().ToString();

            LoadTask();
        }

        private async void Day5_Tapped(object sender, EventArgs e)
        {
            ColorDefault();

            ElementDay5.BackgroundColor = Color.Gray;
            Day5.TextColor = Color.Black;
            DayOfWeek5.TextColor = Color.Black;

            CurrentDate = DateTime.Now.AddDays(1).ToShortDateString().ToString();

            LoadTask();
        }

        private async void Day6_Tapped(object sender, EventArgs e)
        {
            ColorDefault();

            ElementDay6.BackgroundColor = Color.Gray;
            Day6.TextColor = Color.Black;
            DayOfWeek6.TextColor = Color.Black;

            CurrentDate = DateTime.Now.AddDays(2).ToShortDateString().ToString();

            LoadTask();
        }

        private async void Day7_Tapped(object sender, EventArgs e)
        {
            ColorDefault();

            ElementDay7.BackgroundColor = Color.Gray;
            Day7.TextColor = Color.Black;
            DayOfWeek7.TextColor = Color.Black;

            CurrentDate = DateTime.Now.AddDays(3).ToShortDateString().ToString();

            LoadTask();
        }
    }
}