using Note.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;

namespace Note.View
{
    public partial class CalendarPage : ContentPage
    {
        private bool _value;
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
            }
        }

        public CalendarPage()
        {

            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            LoadNotification();
            base.OnAppearing();
            
        }

        public async void LoadNotification()
        {
            Value = false;


            CalendarTest.ShownDate = DateTime.Now;
            CalendarTest.EventIndicatorColor = Color.Purple;

            var notifications = await App.NoteDB.GetNotificationAsync();

            
            var test = new EventCollection();
            foreach (var i in notifications.Select(e => e.DateNotification).Distinct()) 
            {
                test.Add(DateTime.Parse(i), await App.NoteDB.GetNotificationDateAsync(i));
            }
            CalendarTest.Events = test;
        }

        private async void ButtonClick_LoadNote(object sender, EventArgs e)
        {

            CalendarTest.Events.Clear();
            CalendarTest.ShownDate = DateTime.Now;
            CalendarTest.EventIndicatorColor = Color.Purple;

            var notifications = await App.NoteDB.GetNoteAsync();


            var test = new EventCollection();
            foreach (var i in notifications.Select(p => p.Date).Distinct())
            {
                test.Add(DateTime.Parse(i), await App.NoteDB.GetNoteDateAsync(i));
            }
            CalendarTest.Events = test;
        }

        private void ButtonClick_LoadNotification(object sender, EventArgs e)
        {
            CalendarTest.Events.Clear();
            LoadNotification();
        }


        private async void DeleteNoteCommand(object sender, EventArgs e)
        {
            NotificationsModel notification = (NotificationsModel)((SwipeItem)sender).BindingContext;
            await App.NoteDB.DeleteNotificationAsync(notification);
            LoadNotification();
        }

        private async void ChangeNotificationCommand(object sender, EventArgs e)
        {
            NotificationsModel notification = (NotificationsModel)((SwipeItem)sender).BindingContext;
            await Shell.Current.GoToAsync(
                   $"{nameof(NotificationAddingPage)}?{nameof(NotificationAddingPage.ItemId)}={notification.ID.ToString()}");
        }

        private async void ButtonClick_LoadTask(object sender, EventArgs e)
        {
            CalendarTest.Events.Clear();
            CalendarTest.ShownDate = DateTime.Now;
            CalendarTest.EventIndicatorColor = Color.Purple;
            var notifications = await App.NoteDB.GetTaskAsync();


            var test = new EventCollection();
            foreach (var i in notifications.Select(p => p.Date).Distinct())
            {
                test.Add(DateTime.Parse(i), await App.NoteDB.GetTaskDateAsync(i));
            }
            CalendarTest.Events = test;
        }
    }
}