using Note.Model;
using Note.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace Note.ViewModel
{
    public class NoteViewModel : BaseViewModel
    {
        public NoteViewModel() 
        {
            LoadNote();
            Navigation = new Command(NavigationNote);
            Selection = new Command(OnSelectionChanged);
            Delete = new Command(DeleteNote);
        }   

        #region Подгрузка Заметок
        public async void LoadNote() 
        {
            Notes = await App.NoteDB.GetNoteAsync();
        }

        private List<NoteModel> _notes;

        public List<NoteModel> Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged("Notes");
            }
        }
        #endregion

        private NoteModel _selectedNote;

        public NoteModel SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged("SelectedNote");
             
            }
        }



        #region Команды
        public ICommand Selection { get; }

        public async void OnSelectionChanged()
        {
            if (SelectedNote != null)
            {
                NoteModel note = SelectedNote;
                await Shell.Current.GoToAsync(
                           $"{nameof(NoteAddingPage)}?{nameof(NoteAddingPage.ItemId)}={note.ID.ToString()}");
            }
        }

        public ICommand Navigation { get; }

        public async void NavigationNote()
        {
            await Shell.Current.GoToAsync(nameof(NoteAddingPage));
        }

        public ICommand Delete { get; }

        public async void DeleteNote()
        {
            if (SelectedNote != null) 
            {
                NoteModel note = SelectedNote;
                await App.NoteDB.DeleteNoteAsync(note);
            }
                
            
        }


        #endregion
    }
}
