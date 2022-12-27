using Note.Model;
using Plugin.LocalNotification;
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

            // Здесь код для отображение уведомления
            var notification1 = new NotificationRequest
            {
                NotificationId = notification.ID,
                Title = notification.TitelNotification,
                Description = notification.TextNotification,
                Schedule =
                {
                   NotifyTime = date // Used for Scheduling local notification, if not specified notification will show immediately.
                }
            };
            await LocalNotificationCenter.Current.Show(notification1);


            if (!string.IsNullOrWhiteSpace(notification.TitelNotification))
            {
                await App.NoteDB.SaveNotificationAsync(notification);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            NotificationsModel notification = (NotificationsModel)BindingContext;

            // код для отметы отображение запронированного уведомления
            
            await App.NoteDB.DeleteNotificationAsync(notification);

            await Shell.Current.GoToAsync(".."); // Закрытие страницы задач
        }
    }
}
