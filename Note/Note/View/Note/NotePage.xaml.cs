using Note.Model;
using Note.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Note.View
{
    public partial class NotePage : ContentPage
    {
        public NotePage()
        {
            InitializeComponent();
            BindingContext = new NoteViewModel();
        }

        //protected override async void OnAppearing()
        //{
        //    collectionView.ItemsSource = await App.NoteDB.GetNoteAsync();
        //    base.OnAppearing(); 
        //}

        //private async void DeleteNoteCommand(object sender, EventArgs e)
        //{
        //    NoteModel note = (NoteModel)((SwipeItem)sender).BindingContext;
        //    await App.NoteDB.DeleteNoteAsync(note);
        //    collectionView.ItemsSource = await App.NoteDB.GetNoteAsync();
        //}
    }
}
