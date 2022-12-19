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
    public partial class NotificationPage : ContentPage
    {
        public NotificationPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.NoteDB.GetNotificationAsync();
            base.OnAppearing();
        }

        /// <summary>
        /// Переход на старницу добавления напоминания
        /// </summary>
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NotificationAddingPage));
        }

        /// <summary>
        /// Выбор уведомления
        /// </summary>
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                NotificationsModel notification = (NotificationsModel)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(
                    $"{nameof(NotificationAddingPage)}?{nameof(NotificationAddingPage.ItemId)}={notification.ID.ToString()}");
            }
        }

        /// <summary>
        /// Удаление уведомление по свайпу 
        /// </summary>
        private async void DeleteNoteCommand(object sender, EventArgs e)
        {
            NotificationsModel notification = (NotificationsModel)((SwipeItem)sender).BindingContext;
            await App.NoteDB.DeleteNotificationAsync(notification);
            collectionView.ItemsSource = await App.NoteDB.GetNotificationAsync();
        }
    }
}