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
    public partial class NoteAddingPage : ContentPage
    {

        public string ItemId 
        {
            set 
            {
                LoadNote(value);
            }
        }
    
        public NoteAddingPage()
        {
            InitializeComponent();
            BindingContext = new NoteModel();
        }

        private async void LoadNote(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);
                NoteModel note = await App.NoteDB.GetNoteAsync(id);

                BindingContext = note;
            }
            catch { }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            NoteModel note = (NoteModel)BindingContext;

            note.Date = DateTime.Now.ToShortDateString();

            if (!string.IsNullOrWhiteSpace(note.Title))
            {
                await App.NoteDB.SaveNoteAsync(note);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            NoteModel note = (NoteModel)BindingContext;

            await App.NoteDB.DeleteNoteAsync(note);

            await Shell.Current.GoToAsync(".."); // Закрытие страницы задач
        }
    }
}