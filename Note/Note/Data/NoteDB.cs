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

        public Task<TaskModel> GetTaskAsync(int ID)
        {
            return db.Table<TaskModel>()
                .Where(i => i.ID == ID)
                .FirstOrDefaultAsync();
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
    }
}
