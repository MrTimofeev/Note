using Note.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Data
{
    public class NoteDB
    {
        readonly SQLiteAsyncConnection db;

        public NoteDB(string connectionstring)
        {
            db = new SQLiteAsyncConnection(connectionstring);
            db.CreateTableAsync<NoteModel>().Wait();
            db.CreateTableAsync<TaskModel>().Wait();
            db.CreateTableAsync<NotificationsModel>().Wait();
            db.CreateTableAsync<HabitModel>().Wait();
        }


        //TODO: Потом перенести в отдельный класс
        #region Методы работы с бд для ЗАМЕТОК 
        public Task<List<NoteModel>> GetNoteAsync()
        {
            return db.Table<NoteModel>().ToListAsync();
        }

        public Task<NoteModel> GetNoteAsync(int ID)
        {
            return db.Table<NoteModel>()
                .Where(i => i.ID == ID)
                .FirstOrDefaultAsync();
        }

        public Task<List<NoteModel>> GetNoteDateAsync(string Date)
        {
            return db.Table<NoteModel>()
                .Where(i => i.Date == Date).ToListAsync();
        }

        public Task<int> SaveNoteAsync(NoteModel note)
        {
            if (note.ID != 0)
            {
                return db.UpdateAsync(note);
            }
            else
            {
                return db.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(NoteModel note)
        {
            return db.DeleteAsync(note);
        }
        #endregion  


        //TODO: Потом перенести в отдельный класс
        #region Методы работы с бд для ЗАДАЧ
        public Task<List<TaskModel>> GetTaskAsync()
        {
            return db.Table<TaskModel>().ToListAsync();
        }

        //Выполненые 
        public Task<List<TaskModel>> GetTaskCompleteAsync()
        {
            return db.Table<TaskModel>().Where(x=> x.Status==true).ToListAsync();
        }

        // Не Выполненые 
        public Task<List<TaskModel>> GetTaskNotCompleteAsync()
        {
            return db.Table<TaskModel>().Where(x => x.Status == false).ToListAsync();
        }

        public Task<TaskModel> GetTaskAsync(int ID)
        {
            return db.Table<TaskModel>()
                .Where(i => i.ID == ID)
                .FirstOrDefaultAsync();
        }

        public Task<List<TaskModel>> GetTaskDateAsync(string Date)
        {
            return db.Table<TaskModel>()
                .Where(i => i.Date == Date)
                .ToListAsync();
        }

        public Task<int> SaveTaskAsync(TaskModel task)
        {
            if (task.ID != 0)
            {
                return db.UpdateAsync(task);
            }
            else
            {
                return db.InsertAsync(task);
            }
        }

        public Task<int> DeleteTaskAsync(TaskModel task)
        {
            return db.DeleteAsync(task);
        }
        #endregion


        //TODO: Потом перенести в отдельный класс
        #region Методы работы с бд для Уведомлений
        public Task<List<NotificationsModel>> GetNotificationAsync()
        {
            return db.Table<NotificationsModel>().ToListAsync();
        }

        public Task<List<NotificationsModel>> GetNotificationDateAsync(string Date)
        {
            return db.Table<NotificationsModel>()
                .Where(i => i.DateNotification == Date).ToListAsync();
        }

        public Task<NotificationsModel> GetNotificationAsync(int ID)
        {
            return db.Table<NotificationsModel>()
                .Where(i => i.ID == ID)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveNotificationAsync(NotificationsModel notification)
        {
            if (notification.ID != 0)
            {
                return db.UpdateAsync(notification);
            }
            else
            {
                return db.InsertAsync(notification);
            }
        }

        public Task<int> DeleteNotificationAsync(NotificationsModel notification)
        {
            return db.DeleteAsync(notification);
        }
        #endregion


        //TODO: Потом перенести в отдельный класс
        #region Методы работы с бд для Привычек
        public Task<List<HabitModel>> GetHabitsAsync()
        {
            return db.Table<HabitModel>().ToListAsync();
        }
        #endregion
    }
}
