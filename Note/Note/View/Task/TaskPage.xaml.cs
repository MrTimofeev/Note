using Note.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Note.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskPage : ContentPage
    {
        public List<TaskModel> source;
        
        public List<TaskModel> TaskCollection
        {
            get => source;
            private set
            {
                if (value != source)
                {
                    source = value;
                    OnPropertyChanged(nameof(TaskModel));
                }
            }
        }

        public TaskPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.NoteDB.GetTaskAsync();
            base.OnAppearing();
           //TaskCollection = new List<TaskModel>();
            //TaskCollection = await App.NoteDB.GetTaskAsync();
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
        private async void CheckedChangedTask(object sender, CheckedChangedEventArgs e)
        {
            
            TaskModel task = (TaskModel)((CheckBox)sender).BindingContext;

            if (task.Status == false)
            {
                task.Status = false; // задача не выполнена 
                if (!string.IsNullOrWhiteSpace(task.Text))
                {
                    await App.NoteDB.SaveTaskAsync(task);
                }
            }

            if (task.Status == true)
            {
                task.Status = true; // задача выполнена 
                if (!string.IsNullOrWhiteSpace(task.Text))
                {
                    await App.NoteDB.SaveTaskAsync(task);
                }
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
            collectionView.ItemsSource = await App.NoteDB.GetTaskAsync();
        }
    }
}