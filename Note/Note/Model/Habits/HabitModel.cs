using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Note.Model
{
    public class HabitModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Текст привычки
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата создания привычки
        /// </summary>
        public String Date { get; set; }

        /// <summary>
        /// Статус привычки в определенный день (Зависит от даты)
        /// </summary>
        public bool Status { get; set; }
    }
}
