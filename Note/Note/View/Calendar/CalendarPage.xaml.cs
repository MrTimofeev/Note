using Note.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;

namespace Note.View
{ 
	public partial class CalendarPage : ContentPage
	{
		public CalendarPage()
		{
			InitializeComponent();
            var date = DateTime.Parse("02.20.2023");
			CalendarTest.ShownDate = DateTime.Now;
            CalendarTest.EventIndicatorColor = Color.Purple;
            CalendarTest.Events = new EventCollection
            {
                [date] = new List<NoteModel>
                {
                    new NoteModel { Title = "Cool event1", Text = "This is Cool event1's description!" },
                    new NoteModel { Title = "Cool event2", Text = "This is Cool event2's description!" }
                },
                [DateTime.Now.AddDays(-1)] = new List<NoteModel>
                {
                    new NoteModel { Title = "Cool event1", Text = "This is Cool event1's description!" },
                    new NoteModel { Title = "Cool event2", Text = "This is Cool event2's description!" }
                },
                [DateTime.Now.AddDays(1)] = new List<NoteModel>
                {
                    new NoteModel { Title = "Cool event1", Text = "This is Cool event1's description!" },
                    new NoteModel { Title = "Cool event2", Text = "This is Cool event2's description!" }
                }
            };
        }
    }
}