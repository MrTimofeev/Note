using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace Note.Model
{
    public class NotificationsModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Заголовок уведомления
        /// </summary>
        public string TextNotification { get; set; }

        /// <summary>
        /// Текст уведомления
        /// </summary>
        public string TitelNotification { get; set; }
    }
}
