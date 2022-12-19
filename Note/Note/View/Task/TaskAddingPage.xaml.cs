using Note.Interface;
using Note.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotifications;
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

        /// <summary>
        /// Асинхронный метод загрузки задачи
        /// </summary>
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


        /// <summary>
        /// Сохрание задачи
        /// </summary>
        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            DateTime date1 = new DateTime(2022, 12, 29, 10, 54, 00);
            CrossLocalNotifications.Current.Show("Заголов", "Текст уведомления", 1 , date1); // Пример уведомления 
            CrossLocalNotifications.Current.Show("Заголов", "Текст уведомления", 2, DateTime.Now.AddSeconds(10)); // Пример уведомления 


            TaskModel task = (TaskModel)BindingContext;
            task.Date = DateTime.Now.ToShortDateString(); 
            task.Status = false; // задача не выполнена 
            if (!string.IsNullOrWhiteSpace(task.Text))
            {
                await App.NoteDB.SaveTaskAsync(task);
            }

            await Shell.Current.GoToAsync("..");
        }
        
        /// <summary>
        /// Удаление задачи
        /// </summary>
        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            TaskModel task = (TaskModel)BindingContext;

            await App.NoteDB.DeleteTaskAsync(task);

            await Shell.Current.GoToAsync("..");
        }
    }
}
