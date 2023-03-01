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
        public string TitelNotification { get; set; }

        /// <summary>
        /// Описане уведомления
        /// </summary>
        public string TextNotification { get; set; }

        /// <summary>
        /// Дата отоброжения  уведомления
        /// </summary>
        public string DateNotification { get; set; }

        /// <summary>
        ///  Время отображения уведомления
        /// </summary>
        public TimeSpan TimeNotification { get; set; } 

    }
}
