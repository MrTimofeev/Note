using Note.Interface;
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
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class TaskAddingPage : ContentPage
    {

        public TaskAddingPage()
        {
            InitializeComponent();
            BindingContext = new TaskModel();
        }

        public string ItemId
        {
            set
            {
                LoadTask(value);
            }
        }


        private async void LoadTask(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);
                TaskModel task = await App.NoteDB.GetTaskAsync(id);
                BindingContext = task;
            }
            catch { }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            TaskModel task = (TaskModel)BindingContext;

            task.Date = DateTime.Now.ToShortDateString();

            if (!string.IsNullOrWhiteSpace(task.Text))
            {
                await App.NoteDB.SaveTaskAsync(task);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            TaskModel task = (TaskModel)BindingContext;

            await App.NoteDB.DeleteTaskAsync(task);

            await Shell.Current.GoToAsync("..");
        }
    }
}
