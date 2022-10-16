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
        }

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

    }
}
