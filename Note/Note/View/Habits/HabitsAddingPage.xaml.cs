using Note.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Note.View
{
    [QueryProperty(nameof(ItemText), nameof(ItemText))]
    public partial class HabitsAddingPage : ContentPage
	{
		public HabitsAddingPage()
		{
			InitializeComponent();
            BindingContext = new HabitModel();
		}

        public string ItemText
        {
            set
            {
                LoadHabit(value);
            }
        }

        private async void LoadHabit(string value)
        {
            try
            {
                HabitModel habit = await App.NoteDB.GetHabitAsync(value);

                BindingContext = habit;
            }
            catch { }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            HabitModel habit = (HabitModel)BindingContext;

            habit.Date = DateTime.Now.ToShortDateString();

            habit.Status = false;

            if (!string.IsNullOrWhiteSpace(habit.Text))
            {
                await App.NoteDB.SaveHabitAsync(habit);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            HabitModel habit = (HabitModel)BindingContext;

            await App.NoteDB.DeleteHabitAsync(habit);

            await Shell.Current.GoToAsync(".."); // Закрытие страницы задач
        }
    }
}