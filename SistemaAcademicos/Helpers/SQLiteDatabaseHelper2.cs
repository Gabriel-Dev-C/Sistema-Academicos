using SistemaAcademicos.Models;
using SQLite;

namespace SistemaAcademicos.Helpers
{
    public class SQLiteDatabaseHelper2
    {
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDatabaseHelper2(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Disciplina>().Wait();
        }

        public Task<int> Insert(Disciplina p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Disciplina>> Update(Disciplina p)
        {
            string sql = "UPDATE Disciplina SET Nome=?, Sigla=? WHERE Id=?";

            return _conn.QueryAsync<Disciplina>(sql, p.Nome, p.Sigla, p.Id);
        }

        public Task<List<Disciplina>> GetAll()
        {
            return _conn.Table<Disciplina>().ToListAsync();
        }

        public Task<List<Disciplina>> Search(string p)
        {
            string sql = "SELECT * FROM Disciplina WHERE Nome LIKE '%\" + q + \"%'";

            return _conn.QueryAsync<Disciplina>(sql);
        }

        public Task<int> Delete(int p)
        {
            return _conn.Table<Disciplina>().DeleteAsync(i => i.Id == p);
        }
    }
}
