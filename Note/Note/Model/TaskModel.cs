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

        /// <summary>
        /// Текст задачи 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата выполнения задачи
        /// </summary>
        public String Date { get; set; }

        /// <summary>
        /// Статус задачи ВЫПОЛНЕН/НЕВЫПОЛНЕН
        /// </summary>
        public bool Status { get; set; }
    }
}
