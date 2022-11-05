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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskPage : ContentPage
    {
        public TaskPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.NoteDB.GetTaskAsync();
            base.OnAppearing();
        }

        #region Комнады
        // Команда прехода на страницу добавления
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TaskAddingPage));
        }
        #endregion

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                TaskModel note = (TaskModel)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(
                    $"{nameof(TaskAddingPage)}?{nameof(TaskAddingPage.ItemId)}={note.ID.ToString()}");
            }
        }
    }
}