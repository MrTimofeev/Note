using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Note.Model
{
    public class NoteModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
        
        public string Date { get; set;  }
    }
}
