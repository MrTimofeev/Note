using Note.Model;
using Plugin.LocalNotifications;
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
            

            // данные для сохранения и отображения (сделал именно так ибо по другому не работет)
            notification.DateNotification = DateNotification.Date.ToShortDateString().ToString();
            notification.TimeNotification = TimeNotification.Time;

            // Переменная для создания уведомления
            var date = DateTime.Parse(DateNotification.Date.ToShortDateString().ToString() + " " + TimeNotification.Time.ToString());

            CrossLocalNotifications.Current.Show(notification.TitelNotification, notification.TextNotification, notification.ID, date);

            if (!string.IsNullOrWhiteSpace(notification.TitelNotification))
            {
                await App.NoteDB.SaveNotificationAsync(notification);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            NotificationsModel notification = (NotificationsModel)BindingContext;

            CrossLocalNotifications.Current.Cancel(notification.ID);
            
            await App.NoteDB.DeleteNotificationAsync(notification);

            await Shell.Current.GoToAsync(".."); // Закрытие страницы задач
        }
    }
}
