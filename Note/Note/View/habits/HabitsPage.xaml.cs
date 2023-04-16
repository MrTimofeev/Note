using Note.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Note.View
{
	public partial class HabitsPage : ContentPage
	{
		public HabitsPage ()
		{
			InitializeComponent();
            //collectionView.ItemsSource = "test";

        }


        public void LoadDateHabits() 
        {
            DayOfWeek1.Text = DateTime.Now.DayOfWeek.ToString() == "Monday" ? "Пн" : "Пн";
            Day1.Text = DateTime.Now.Day.ToString();
            DayOfWeek2.Text = DateTime.Now.AddDays(-1).DayOfWeek.ToString() == "Tuesday" ? "Вт" : "Вт";
            Day2.Text = DateTime.Now.AddDays(-1).Day.ToString();
            DayOfWeek3.Text = DateTime.Now.AddDays(-2).DayOfWeek.ToString() == "Wednesday" ? "Ср" : "Ср";
            Day3.Text = DateTime.Now.AddDays(-2).Day.ToString();
            DayOfWeek4.Text = DateTime.Now.AddDays(-3).DayOfWeek.ToString() == "Thursday" ? "Чт" : "Чт";
            Day4.Text = DateTime.Now.AddDays(-3).Day.ToString();
            DayOfWeek5.Text = DateTime.Now.AddDays(-4).DayOfWeek.ToString() == "Friday" ? "Пт" : "Пт";
            Day5.Text = DateTime.Now.AddDays(-4).Day.ToString();
            DayOfWeek6.Text = DateTime.Now.AddDays(-5).DayOfWeek.ToString() == "Saturday" ? "Сб" : "Сб";
            Day6.Text = DateTime.Now.AddDays(-5).Day.ToString();
            DayOfWeek7.Text = DateTime.Now.AddDays(-6).DayOfWeek.ToString() == "Sunday" ? "Вс" : "Вс";
            Day7.Text = DateTime.Now.AddDays(-6).Day.ToString();
        }

        public async void LoadHabits() 
        {
            //var HabitsDB = await App.NoteDB.GetHabitsAsync();
            //collectionView.ItemsSource = await App.NoteDB.GetHabitsAsync();
            var habitDisplay = new List<HabitDisplayModel>();

            var HabitsDB = new List<HabitModel>()
            {
                new HabitModel()
                {
                    ID = 1,
                    Date = DateTime.Now.ToString(),
                    Status = true,
                    Text = "Привычка 1"
                },
                new HabitModel()
                {
                    ID = 2,
                    Date = DateTime.Now.AddDays(-1).ToString(),
                    Status = true,
                    Text = "Привычка 1"
                },
                new HabitModel()
                {
                    ID = 3,
                    Date = DateTime.Now.AddDays(-2).ToString(),
                    Status = true,
                    Text = "Привычка 1"
                },
                new HabitModel()
                {
                    ID = 4,
                    Date = DateTime.Now.AddDays(-3).ToString(),
                    Status = true,
                    Text = "Привычка 1"
                },
                 new HabitModel()
                {
                    ID = 1,
                    Date = DateTime.Now.ToString(),
                    Status = true,
                    Text = "Привычка 2"
                },
                new HabitModel()
                {
                    ID = 2,
                    Date = DateTime.Now.AddDays(-1).ToString(),
                    Status = true,
                    Text = "Привычка 3"
                },
                new HabitModel()
                {
                    ID = 3,
                    Date = DateTime.Now.AddDays(-2).ToString(),
                    Status = true,
                    Text = "Привычка 4"
                },
                new HabitModel()
                {
                    ID = 4,
                    Date = DateTime.Now.AddDays(-3).ToString(),
                    Status = true,
                    Text = "Привычка 5"
                },


            };

            var UniqueHabit = new List<string>();
            HabitsDB.ForEach(x=> UniqueHabit.Add(x.Text));

            UniqueHabit.Distinct().ForEach(x => habitDisplay.Add(new HabitDisplayModel()
            {
                Text = x,
                StatusDay1 = HabitsDB.Any(q=>q.Date == DateTime.Now.ToString() && x == q.Text),
                StatusDay2 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-1).ToString() && x == q.Text),
                StatusDay3 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-2).ToString() && x == q.Text),
                StatusDay4 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-3).ToString() && x == q.Text),
                StatusDay5 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-4).ToString() && x == q.Text),
                StatusDay6 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-5).ToString() && x == q.Text),
                StatusDay7 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-6).ToString() && x == q.Text),

            })) ;

            collectionView.ItemsSource = habitDisplay;
        }

        protected override async void OnAppearing()
        {
            LoadDateHabits();
            LoadHabits();
            base.OnAppearing();
        }

        private void CheckBox_CheckedChanged1(object sender, CheckedChangedEventArgs e)
        {
            
        }
        private void CheckBox_CheckedChanged2(object sender, CheckedChangedEventArgs e)
        {
           
        }
        private void CheckBox_CheckedChanged3(object sender, CheckedChangedEventArgs e)
        {
            
        }
        private void CheckBox_CheckedChanged4(object sender, CheckedChangedEventArgs e)
        {
            
        }
        private void CheckBox_CheckedChanged5(object sender, CheckedChangedEventArgs e)
        {
          
        }
        private void CheckBox_CheckedChanged6(object sender, CheckedChangedEventArgs e)
        {
           
        }
        private void CheckBox_CheckedChanged7(object sender, CheckedChangedEventArgs e)
        {
            
        }

    }
}