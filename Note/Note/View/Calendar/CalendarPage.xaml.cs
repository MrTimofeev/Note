using Note.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;

namespace Note.View
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {

            InitializeComponent();
            //LoadNote();
        }

        protected override async void OnAppearing()
        {
            LoadNote();
            base.OnAppearing();
        }

        public async void LoadNote()
        {
            
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
    }
}