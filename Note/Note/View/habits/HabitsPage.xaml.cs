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
            var HabitsDB = await App.NoteDB.GetHabitsAsync();
            
            var habitDisplay = new List<HabitDisplayModel>();
            
            var UniqueHabit = new List<string>(); // Названия привычек
            HabitsDB.ForEach(x=> UniqueHabit.Add(x.Text));

            // провека на уникаольность и перебор
            UniqueHabit.Distinct().ForEach(x => habitDisplay.Add(new HabitDisplayModel() 
            {
                Text = x,
                StatusDay1 = HabitsDB.Any(q=>q.Date == DateTime.Now.ToShortDateString() && x == q.Text && q.Status == true),
                StatusDay2 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-1).ToShortDateString() && x == q.Text),
                StatusDay3 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-2).ToShortDateString() && x == q.Text),
                StatusDay4 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-3).ToShortDateString() && x == q.Text),
                StatusDay5 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-4).ToShortDateString() && x == q.Text),
                StatusDay6 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-5).ToShortDateString() && x == q.Text),
                StatusDay7 = HabitsDB.Any(q => q.Date == DateTime.Now.AddDays(-6).ToShortDateString() && x == q.Text),

            })) ;

            collectionView.ItemsSource = habitDisplay;
            
        }

        protected override async void OnAppearing()
        {
            LoadDateHabits();
            LoadHabits();
            base.OnAppearing();
        }

        private async void CheckBox_CheckedChanged1(object sender, CheckedChangedEventArgs e)
        {
            HabitDisplayModel habit = (HabitDisplayModel)((CheckBox)sender).BindingContext; //Все данные выбранной привычки

            if (habit == null) { return; } //Это чтобы ошику не выбивало

            var AllHabit = await App.NoteDB.GetHabitsAsync();// Все првычки в базе
           
            var CompleteHabit = new HabitModel() // Переменная которую либо удалим либо добавим
            {
                Status = true,
                Date = DateTime.Now.ToShortDateString(),
                Text = habit.Text,
            };

            if (habit.StatusDay1 == true && !AllHabit.Any(x=>x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status)) //Если статус изменился сохраняем в базу иначе проверяем есть ли данная запись и удаляем 
            {
                await App.NoteDB.SaveHabitAsync(CompleteHabit);
            }
            
            if (habit.StatusDay1 == false && AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status)) 
            {
                await App.NoteDB.DeleteHabitAsync(AllHabit.FirstOrDefault(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status));
            }
        }
        private async void CheckBox_CheckedChanged2(object sender, CheckedChangedEventArgs e)
        {
            HabitDisplayModel habit = (HabitDisplayModel)((CheckBox)sender).BindingContext; //Все данные выбранной привычки

            if (habit == null) { return; } //Это чтобы ошику не выбивало

            var AllHabit = await App.NoteDB.GetHabitsAsync();// Все првычки в базе

            var CompleteHabit = new HabitModel() // Переменная которую либо удалим либо добавим
            {
                Status = true,
                Date = DateTime.Now.AddDays(-1).ToShortDateString(),
                Text = habit.Text,
            };

            if (habit.StatusDay2 == true && !AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status)) //Если статус изменился сохраняем в базу иначе проверяем есть ли данная запись и удаляем 
            {
                await App.NoteDB.SaveHabitAsync(CompleteHabit);
            }
            
            if (habit.StatusDay2 == false && AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status))
            {
                await App.NoteDB.DeleteHabitAsync(AllHabit.FirstOrDefault(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status));
            }

        }
        private async void CheckBox_CheckedChanged3(object sender, CheckedChangedEventArgs e)
        {
            HabitDisplayModel habit = (HabitDisplayModel)((CheckBox)sender).BindingContext; //Все данные выбранной привычки

            if (habit == null) { return; } //Это чтобы ошику не выбивало

            var AllHabit = await App.NoteDB.GetHabitsAsync();// Все првычки в базе

            var CompleteHabit = new HabitModel() // Переменная которую либо удалим либо добавим
            {
                Status = true,
                Date = DateTime.Now.AddDays(-2).ToShortDateString(),
                Text = habit.Text,
            };

            if (habit.StatusDay3 == true && !AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status)) //Если статус изменился сохраняем в базу иначе проверяем есть ли данная запись и удаляем 
            {
                await App.NoteDB.SaveHabitAsync(CompleteHabit);
            }
            
            if (habit.StatusDay3 == false && AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status))
            {
                await App.NoteDB.DeleteHabitAsync(AllHabit.FirstOrDefault(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status));
            }
        }
        private async void CheckBox_CheckedChanged4(object sender, CheckedChangedEventArgs e)
        {
            HabitDisplayModel habit = (HabitDisplayModel)((CheckBox)sender).BindingContext; //Все данные выбранной привычки

            if (habit == null) { return; } //Это чтобы ошику не выбивало

            var AllHabit = await App.NoteDB.GetHabitsAsync();// Все првычки в базе

            var CompleteHabit = new HabitModel() // Переменная которую либо удалим либо добавим
            {
                Status = true,
                Date = DateTime.Now.AddDays(-3).ToShortDateString(),
                Text = habit.Text,
            };

            if (habit.StatusDay4 == true && !AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status)) //Если статус изменился сохраняем в базу иначе проверяем есть ли данная запись и удаляем 
            {
                await App.NoteDB.SaveHabitAsync(CompleteHabit);
            }

            if (habit.StatusDay4 == false && AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status))
            {
                await App.NoteDB.DeleteHabitAsync(AllHabit.FirstOrDefault(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status));
            }
        }
        private async void CheckBox_CheckedChanged5(object sender, CheckedChangedEventArgs e)
        {
            HabitDisplayModel habit = (HabitDisplayModel)((CheckBox)sender).BindingContext; //Все данные выбранной привычки

            if (habit == null) { return; } //Это чтобы ошику не выбивало

            var AllHabit = await App.NoteDB.GetHabitsAsync();// Все првычки в базе

            var CompleteHabit = new HabitModel() // Переменная которую либо удалим либо добавим
            {
                Status = true,
                Date = DateTime.Now.AddDays(-4).ToShortDateString(),
                Text = habit.Text,
            };

            if (habit.StatusDay5 == true && !AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status)) //Если статус изменился сохраняем в базу иначе проверяем есть ли данная запись и удаляем 
            {
                await App.NoteDB.SaveHabitAsync(CompleteHabit);
            }

            if (habit.StatusDay5 == false && AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status))
            {
                await App.NoteDB.DeleteHabitAsync(AllHabit.FirstOrDefault(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status));
            }
        }
        private async void CheckBox_CheckedChanged6(object sender, CheckedChangedEventArgs e)
        {
            HabitDisplayModel habit = (HabitDisplayModel)((CheckBox)sender).BindingContext; //Все данные выбранной привычки

            if (habit == null) { return; } //Это чтобы ошику не выбивало

            var AllHabit = await App.NoteDB.GetHabitsAsync();// Все првычки в базе

            var CompleteHabit = new HabitModel() // Переменная которую либо удалим либо добавим
            {
                Status = true,
                Date = DateTime.Now.AddDays(-5).ToShortDateString(),
                Text = habit.Text,
            };

            if (habit.StatusDay6 == true && !AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status)) //Если статус изменился сохраняем в базу иначе проверяем есть ли данная запись и удаляем 
            {
                await App.NoteDB.SaveHabitAsync(CompleteHabit);
            }

            if (habit.StatusDay6 == false && AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status))
            {
                await App.NoteDB.DeleteHabitAsync(AllHabit.FirstOrDefault(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status));
            }
        }
        private async void CheckBox_CheckedChanged7(object sender, CheckedChangedEventArgs e)
        {
            HabitDisplayModel habit = (HabitDisplayModel)((CheckBox)sender).BindingContext; //Все данные выбранной привычки

            if (habit == null) { return; } //Это чтобы ошику не выбивало

            var AllHabit = await App.NoteDB.GetHabitsAsync();// Все првычки в базе

            var CompleteHabit = new HabitModel() // Переменная которую либо удалим либо добавим
            {
                Status = true,
                Date = DateTime.Now.AddDays(-6).ToShortDateString(),
                Text = habit.Text,
            };

            if (habit.StatusDay7 == true && !AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status)) //Если статус изменился сохраняем в базу иначе проверяем есть ли данная запись и удаляем 
            {
                await App.NoteDB.SaveHabitAsync(CompleteHabit);
            }

            if (habit.StatusDay7 == false && AllHabit.Any(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status))
            {
                await App.NoteDB.DeleteHabitAsync(AllHabit.FirstOrDefault(x => x.Text == CompleteHabit.Text && x.Date == CompleteHabit.Date && x.Status == CompleteHabit.Status));
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(HabitsAddingPage));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                HabitDisplayModel habit = (HabitDisplayModel)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(
                    $"{nameof(HabitsAddingPage)}?{nameof(HabitsAddingPage.ItemText)}={habit.Text}");
            }
        }
    }
}