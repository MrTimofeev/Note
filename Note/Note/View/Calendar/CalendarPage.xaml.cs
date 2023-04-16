using Note.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;

namespace Note.View
{
    public partial class CalendarPage : ContentPage
    {

        public CalendarPage()
        {

            InitializeComponent();
            CalendarTest.Culture = new CultureInfo("ru-RU");
        }

        protected override void OnAppearing()
        {
            LoadNotification();
            base.OnAppearing();
            
        }

        #region Методы подгрузки

        #region Уведомления
        public async void LoadNotification()
        {
            CalendarTest.EventTemplate = Notification;

            CalendarTest.ShownDate = DateTime.Now;
            CalendarTest.EventIndicatorColor = Color.Purple;

            var notifications = await App.NoteDB.GetNotificationAsync();


            var events = new EventCollection();
            foreach (var i in notifications.Select(e => e.DateNotification).Distinct())
            {
                events.Add(DateTime.Parse(i), await App.NoteDB.GetNotificationDateAsync(i));
            }
            CalendarTest.Events = events;
        }
        #endregion

        #region Заметки 
        public async void LoadNote()
        {
            CalendarTest.EventTemplate = Note;
            CalendarTest.Events.Clear();
            CalendarTest.ShownDate = DateTime.Now;
            CalendarTest.EventIndicatorColor = Color.Purple;

            var note = await App.NoteDB.GetNoteAsync();


            var events = new EventCollection();
            foreach (var i in note.Select(p => p.Date).Distinct())
            {
                events.Add(DateTime.Parse(i), await App.NoteDB.GetNoteDateAsync(i));
            }
            CalendarTest.Events = events;
        }
        #endregion

        #region Задачи
        public async void LoadTask()
        {
            CalendarTest.EventTemplate = Task;
            CalendarTest.Events.Clear();

            CalendarTest.ShownDate = DateTime.Now;
            CalendarTest.EventIndicatorColor = Color.Purple;
            var task = await App.NoteDB.GetTaskAsync();


            var events = new EventCollection();
            foreach (var i in task.Select(p => p.Date).Distinct())
            {
                events.Add(DateTime.Parse(i), await App.NoteDB.GetTaskDateAsync(i));
            }
            CalendarTest.Events = events;
        }

        #endregion

        #endregion


        #region команды обновление 
        private void ButtonClick_LoadNote(object sender, EventArgs e)
        {
            LoadNote();
        }

        private void ButtonClick_LoadNotification(object sender, EventArgs e)
        {
            CalendarTest.Events.Clear();
            LoadNotification();
        }

        private async void ButtonClick_LoadTask(object sender, EventArgs e)
        {

            LoadTask();
        }

        #endregion


        #region команды Заметок
        private async void DeleteNoteCommand(object sender, EventArgs e)
        {
            NoteModel note = (NoteModel)((SwipeItem)sender).BindingContext;
            await App.NoteDB.DeleteNoteAsync(note);
            LoadNote();
        }

        private async void ChangeNoteCommand(object sender, EventArgs e)
        {
            NoteModel note = (NoteModel)((SwipeItem)sender).BindingContext;
            await Shell.Current.GoToAsync(
                   $"{nameof(NoteAddingPage)}?{nameof(NoteAddingPage.ItemId)}={note.ID.ToString()}");
        }
        #endregion


        #region команды Уведомлений
        private async void ChangeNotificationCommand(object sender, EventArgs e)
        {
            NotificationsModel notification = (NotificationsModel)((SwipeItem)sender).BindingContext;
            await Shell.Current.GoToAsync(
                   $"{nameof(NotificationAddingPage)}?{nameof(NotificationAddingPage.ItemId)}={notification.ID.ToString()}");
        }

        private async void DeleteNotificationCommand(object sender, EventArgs e)
        {
            NotificationsModel notification = (NotificationsModel)((SwipeItem)sender).BindingContext;
            await App.NoteDB.DeleteNotificationAsync(notification);
            LoadNotification();

        }
        #endregion


        #region команды Задач

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

        private async void ChangeTaskCommand(object sender, EventArgs e)
        {
            TaskModel task = (TaskModel)((SwipeItem)sender).BindingContext;
            await Shell.Current.GoToAsync(
                   $"{nameof(TaskAddingPage)}?{nameof(TaskAddingPage.ItemId)}={task.ID.ToString()}");
        }

        private async void DeleteTaskCommand(object sender, EventArgs e)
        {
            TaskModel task = (TaskModel)((SwipeItem)sender).BindingContext;
            await App.NoteDB.DeleteTaskAsync(task);
            LoadTask();
        }

        #endregion

        
    }
}