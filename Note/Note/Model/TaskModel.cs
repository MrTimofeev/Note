using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace Note.Model
{
    public class TaskModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Text { get; set; }

        public String Date { get; set; }
    }
}
