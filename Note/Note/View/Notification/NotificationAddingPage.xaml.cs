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
    public partial class NotificationAddingPage : ContentPage
    {
        public NotificationAddingPage()
        {
            InitializeComponent();
            BindingContext = new NotificationsModel();
        }

        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        private async void LoadNote(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);
                NotificationsModel notification = await App.NoteDB.GetNotificationAsync(id);
                BindingContext = notification;
            }
            catch { }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            NotificationsModel notification = (NotificationsModel)BindingContext;

            //notification.DateNotification = DateTime.Now.ToShortDateString();

            if (!string.IsNullOrWhiteSpace(notification.TitelNotification))
            {
                await App.NoteDB.SaveNotificationAsync(notification);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            NotificationsModel notification = (NotificationsModel)BindingContext;

            await App.NoteDB.DeleteNotificationAsync(notification);

            await Shell.Current.GoToAsync(".."); // Закрытие страницы задач
        }
    }
}
