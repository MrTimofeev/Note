using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Note.Data;
using System.IO;

namespace Note
{
    public partial class App : Application
    {

        static NoteDB noteDB;

        public static NoteDB NoteDB 
        {
            get 
            {
                if (noteDB == null)
                { 
                    noteDB =  new NoteDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NotesDataBase.db3"));
                }
                return noteDB;
            } 
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
